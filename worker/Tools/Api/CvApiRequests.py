import requests

BASE_URL = "http://backend"
API_KEY = "PYTHON_SECRET_123"

from pathlib import Path


def Sevice_Token():


    r = requests.post(
        f"{BASE_URL}/api/service-token",
        headers={"X-API-KEY": API_KEY}
    )

    r.raise_for_status()
    token = r.json()["token"]
    print("TOKEN:", token)
    return  token

def UploadGeneratedCV(userID,CV_PATH,companyName):
    token = Sevice_Token()

    headers = {"Authorization": f"Bearer {token}"}

    with open(CV_PATH, "rb") as f:
        files = {"file": ("cv.pdf", f, "application/pdf")}
        data = {"userId": userID , 'companyName': companyName}

        resp = requests.post(f"{BASE_URL}/api/UploadCV", headers=headers, files=files, data=data)

    print(resp.status_code)
    print(resp.text)

def EndProcessError(userID):
    token = Sevice_Token()

    headers = {"Authorization": f"Bearer {token}"}


    data = {"userId": userID , 'status': 2}
    resp = requests.post(f"{BASE_URL}/api/EndFailedProcessCV", headers=headers, data=data)

    print(resp.status_code)
    print(resp.text)