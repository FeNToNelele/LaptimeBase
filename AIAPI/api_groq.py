from flask import Flask, request, jsonify
import os
from groq import Groq
from langchain_core.prompts import ChatPromptTemplate
from langchain_groq import ChatGroq

app = Flask(__name__)

client = ChatGroq(
    temperature=0.8, api_key=os.environ.get("LAPTIMEBASE_API_KEY"), model_name="llama-3.3-70b-versatile"
)

system_prompt = str("You are a helpful assistant racing coach and telemetry analizer. " +
                    "You are going to receive questions about analizing a training session on a specific race track. " +
                    "Give useful insights to the user that are not self-explanatory. Short answer, polite, friendly, willing to help.")

human_prompt= "{text}"

prompt = ChatPromptTemplate.from_messages([("system", system_prompt), ("human", human_prompt)])

chain = prompt | client

@app.route('/ask', methods=['POST'])
def ask_llm():
    data = request.get_json()
    if not data or 'question' not in data:
        return jsonify({"error": "Missing question in request"}), 400
    
    question = data['question']

    try:
        response = chain.invoke({"text" : question})
        return jsonify({"answer": response.content}), 200
    except Exception as e:
        return jsonify({"error": str(e)}), 500

if __name__ == '__main__':
    app.run(port=5000)