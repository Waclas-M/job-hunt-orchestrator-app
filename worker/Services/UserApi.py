import requests
import json
BASE_URL = "http://backend"
API_KEY = "PYTHON_SECRET_123"

from pathlib import Path

THIS_DIR = Path(__file__).resolve().parent.parent.parent
CV_PATH = THIS_DIR / "Workflow/{}/cv_output.pdf"   # Tools/Api/cv.pdf

def Sevice_Token():


    r = requests.post(
        f"{BASE_URL}/api/service-token",
        headers={"X-API-KEY": API_KEY}
    )

    r.raise_for_status()
    token = r.json()["token"]
    print(token)
    return  token

def GetUserData(userId):
    token = Sevice_Token()
    headers = {"Authorization": f"Bearer {token}"}
    resp = requests.get(f"{BASE_URL}/api/UserCvDetailsData?userId={userId}", headers=headers)


    return json.loads(resp.text)

# def GetUserEducationesByIds(userId,ids):
#     token = Sevice_Token()
#     headers = {"Authorization": f"Bearer {token}"}
#     EducationList = []
#     for id in ids:
#         resp = requests.get(f"{BASE_URL}/api/UsersEducationById?userId={userId}?id={id}", headers=headers)
#         EducationList.append(dict(json.loads(resp.text)))
#
#     return EducationList
#
# def GetUserExperiencesByIds(userId,ids):
#     token = Sevice_Token()
#     headers = {"Authorization": f"Bearer {token}"}
#     ExperiencesList = []
#     for id in ids:
#         resp = requests.get(f"{BASE_URL}/api/UsersExperiencesById?userId={userId}?id={id}", headers=headers)
#         ExperiencesList.append(dict(json.loads(resp.text)))
#
#     return ExperiencesList


# def UploadGeneratedCV(userID,CV_PATH_ending):
#     token = Sevice_Token()
#     CV_PATH = THIS_DIR / CV_PATH_ending
#     headers = {"Authorization": f"Bearer {token}"}
#
#     with open(CV_PATH, "rb") as f:
#         files = {"file": ("cv.pdf", f, "application/pdf")}
#         data = {"userId": userID}
#
#         resp = requests.post(f"{BASE_URL}/api/UploadCV", headers=headers, files=files, data=data)