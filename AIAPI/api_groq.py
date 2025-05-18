from flask import Flask, request, jsonify
import os
from langchain_core.prompts import ChatPromptTemplate
from langchain_groq import ChatGroq

app = Flask(__name__)

# LLM client with Groq
client = ChatGroq(
    temperature=0.8,
    api_key=os.environ.get("LAPTIMEBASE_API_KEY"),
    model_name="llama3-70b-8192"
)

# Static system instruction
system_prompt = (
    "You are a helpful assistant racing coach and telemetry analyst. "
    "You are going to receive questions about analyzing a training session on a specific race track. "
    "Give useful insights to the user that are not self-explanatory. "
    "Short answer, polite, friendly, willing to help."
)

# The user prompt includes the question and structured session data
human_prompt = """User question: {question}

Session data:
Held at: {held_at}
Ambient temp: {ambient_temp} °C
Track temp: {track_temp} °C
Track: {track_name}, layout: {track_layout}, length: {track_length_km} km

Laptimes:
{laptimes}
"""

prompt = ChatPromptTemplate.from_messages([("system", system_prompt), ("human", human_prompt)])
chain = prompt | client

@app.route('/ask', methods=['POST'])
def ask_llm():
    data = request.get_json()

    if not data or 'question' not in data or 'session' not in data:
        return jsonify({"error": "Missing 'question' or 'session' in request"}), 400

    question = data['question']
    session = data['session']

    try:
        track = session.get('track', {})
        laptimes_list = session.get('laptimes', [])
        formatted_laptimes = "\n".join([
            f"Lap {lap.get('lapNumber')}: S1={lap.get('sector1')}s, S2={lap.get('sector2')}s, S3={lap.get('sector3')}s, Total={lap.get('total')}s"
            for lap in laptimes_list
        ])

        inputs = {
            "question": question,
            "held_at": session.get('heldAt'),
            "ambient_temp": session.get('ambientTemp'),
            "track_temp": session.get('trackTemp'),
            "track_name": track.get('name'),
            "track_layout": track.get('layout'),
            "track_length_km": track.get('lengthKm'),
            "laptimes": formatted_laptimes
        }

        response = chain.invoke(inputs)
        return jsonify({"answer": response.content}), 200

    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(port=5000)
