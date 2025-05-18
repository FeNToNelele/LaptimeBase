from flask import Flask, request, jsonify
import os
from langchain_core.prompts import ChatPromptTemplate
from langchain_groq import ChatGroq

app = Flask(__name__)

# LLM client with Groq
client = ChatGroq(
    temperature=0.8,
    api_key=os.environ.get("LAPTIMEBASE_API_KEY"),
    model_name="llama-3.3-70b-versatile"
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
Track: {track_name}

Laptimes:
{laptimes}
"""

prompt = ChatPromptTemplate.from_messages([("system", system_prompt), ("human", human_prompt)])
chain = prompt | client

@app.route('/ask', methods=['POST'])
def ask_llm():
    data = request.get_json()

    question = data['question']
    additional_data = data['additional_data']

    track = additional_data.get('track', {})
    laptimes_list = additional_data.get('laptimes', [])
    
    formatted_laptimes = "\n".join([
        f"Team {lap.get('team', {}).get('name', 'Unknown')} ({lap.get('team', {}).get('car', {}).get('class', 'Unknown class')}): {lap.get('time')}"
        for lap in laptimes_list
    ])

    print(len(formatted_laptimes))

    if len(formatted_laptimes) > 41840:
        formatted_laptimes = formatted_laptimes[:41840]

    inputs = {
        "question": question,
        "held_at": additional_data.get('heldAt'),
        "ambient_temp": additional_data.get('ambientTemp'),
        "track_temp": additional_data.get('trackTemp'),
        "track_name": track.get('name'),
        "laptimes": formatted_laptimes
    }

    try:
        response = chain.invoke(inputs)
        return jsonify({"answer": response.content}), 200

    except Exception as e:
        print(e)
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(port=5000)
