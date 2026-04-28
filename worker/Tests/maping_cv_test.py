# from Tools.PDF.mappers.cv_data_mapper import CvDataMapper
#
#
# raw_data = {
#     "UserId": "8ac7d0b1-725c-4f0f-a11a-7f7f7dc89dac",
#     "OfferURL": "https://www.pracuj.pl/praca/internship-5g-ran-cloud-test-automation-engineer-krakow-aleja-generala-tadeusza-bora-komorowskiego-25c,oferta,1004606802?sug=sg_latest_bd_2",
#     "CvForm": 0,
#     "PersonalData": {
#         "ProfileId": 3002,
#         "FirstName": "UżytkownikTestowy1",
#         "LastName": "Testowy",
#         "Email": "email2@gmail.com",
#         "PhoneNumber": None,
#         "PersonalProfile": None,
#         "GitHubURL": "https://github.com/Waclas-M",
#         "LinkedInURL": "www.linkedin.com/in/marcin-węcłaś-594053322"
#     },
#     "UserEducationsProcessAuto": True,
#     "UserExperiencesProcessAuto": True,
#     "UserStrengsProcessAuto": True,
#     "UserSkillsProcessAuto": True,
#     "Education": [
#         {
#             "ProfileId": 3002,
#             "SchoolName": "WWSI semestr 4",
#             "StudyProfile": "Inżynieria Oprogramowania",
#             "StartDate": "2024-01-01",
#             "EndDate": None,
#             "IsCurrent": True
#         }
#     ],
#     "Experience": [
#         {
#             "ProfileId": 3002,
#             "CompanyName": "Pko leasing",
#             "JobDescription": "Wykonywane czynności:\n- Testowanie funkcjonalne.\n- Testy regresji.\n- Tworzenie scenariuszy testowych w Jira.",
#             "StartDate": "2024-05-01",
#             "EndDate": None,
#             "JobTitle": "Tester",
#             "IsCurrent": True
#         }
#     ],
#     "Strengs": [
#         {
#             "ProfileId": 3002,
#             "Strength": "TERAW TEST"
#         }
#     ],
#     "Skills": [
#         {
#             "ProfileId": 3002,
#             "Skill": "Tworzenie MAS skalowany system"
#         }
#     ],
#     "Languages": [
#         {
#             "ProfileId": 3002,
#             "Language": "Angielski",
#             "Level": "1"
#         },
#         {
#             "ProfileId": 3002,
#             "Language": "Polski",
#             "Level": "3"
#         }
#     ],
#     "Interests": [
#         {
#             "ProfileId": 3002,
#             "Interest": "Joga",
#             "Description": "TRARA ZIELEONA"
#         }
#     ]
# }
#
# mapper = CvDataMapper()
# cv_document = mapper.map_from_dict(raw_data)
#
# print(cv_document)