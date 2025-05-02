from flask import Flask, request, jsonify
import os
from groq import Groq

app = Flask(__name__)

client = Groq(
    api_key=os.environ.get("LAPTIMEBASE_API_KEY"),
)

@app.route('/ask', methods=['POST'])
def ask_llm():
    data = request.get_json()
    if not data or 'question' not in data:
        return jsonify({"error": "Missing 'question' in request"}), 400
    
    question = data['question']
    try:
        chat_completion = client.chat.completions.create(
            messages=[
                {
                    "role": "system",
                    "content": "You are a helpful assistant racing coach and telemetry analizer. " +
                    "You are going to receive questions about analizing a training session on a specific race track. " +
                    "Give useful insights to the user that are not self-explanatory. Short answer, polite, friendly, willing to help."
                },
                {
                    "role": "user",
                    "content": question,
                }
            ],
            model="llama-3.3-70b-versatile",  # Example model, adjust according to available models
        )
        response = chat_completion.choices[0].message.content
        return jsonify({"answer": response}), 200
    except Exception as e:
        # Handle exceptions based on the actual exceptions raised by the Groq client.
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(port=5000)