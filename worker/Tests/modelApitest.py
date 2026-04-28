from openai import OpenAI
from dotenv import load_dotenv
import os
import json


load_dotenv()
client = OpenAI(base_url=os.getenv("BASE_URL"),api_key="dummy")




def ApiTest(prompt,Response_format):

    messages = [{"role":"user","content":prompt}]

    try:
        response = client.chat.completions.create(
            model = os.getenv("MODEL"),
            messages = messages,
            response_format = Response_format,
            temperature= 0,
            top_p=0
        )

        content = response.choices[0].message.content
        data = json.loads(content)

        print("\n===== RAW RESPONSE =====")
        print(content)
        print("\n===== RAW RESPONSE REPR =====")
        print(repr(content))

        print(data)

        print("Parsowany JSON:")
        print(json.dumps(data, indent=2, ensure_ascii=False))
        return data
    except Exception as e:
        print("\n===== PROMPT =====\n")
        print(prompt)
        print("\n===== ERROR TYPE =====\n")
        print(type(e).__name__)
        print("\n===== ERROR =====\n")
        print(str(e))