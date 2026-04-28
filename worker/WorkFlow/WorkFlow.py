from  Services import UserApi
import os
import json
from Tools.Api.CvApiRequests import UploadGeneratedCV,EndProcessError
from pathlib import Path
# from Tools.PDF.generator.cv_generator import CvGenerator
from Tools.PDF.mappers.cv_data_mapper import CvDataMapper
from Tools.PDF.templates.classic_green_cv_template import build_pdf
# from Tools.PDF.templates.professional_navy_cv_template import ProfessionalNavyCvTemplate
# from Tests.scrap import ScrapOffer
from Tests.RequestTest import Scrap,AutoDecision

processesChoiceList = ['UserEducationsProcessType','UserExperiencesProcessType','UserStrengsProcessType','UserSkillsProcessType']


def select_by_indexes(data, indexes):
    return [data[i] for i in indexes if 0 <= i < len(data)]

def StartWorkFlow(massage):

        # ścieżka do folderu WorkFlow
        BASE_DIR = Path(__file__).resolve().parent

        # folder generated_cv
        output_dir = BASE_DIR / "generated_cv"

        # utworzenie folderu jeśli nie istnieje
        output_dir.mkdir(parents=True, exist_ok=True)

        # ścieżka do pliku
        output_file = output_dir / "cv_output.pdf"

        try:
            raw_data = json.loads(massage)
            raw_data = dict(raw_data)

            scrap_json = Scrap(raw_data['OfferURL'])
            output = AutoDecision(scrap_json,
                         raw_data['UserEducationsProcessAuto'],
                         raw_data['UserExperiencesProcessAuto'],
                         raw_data['UserStrengsProcessAuto'],
                         raw_data['UserSkillsProcessAuto'],
                         raw_data['Education'],
                         raw_data['Experience'],
                         raw_data['Skills'],
                         raw_data['Strengs'],
                         )

            print(output)

            if raw_data['UserEducationsProcessAuto'] == True:
                raw_data['Education'] = select_by_indexes(raw_data['Education'], output["EducationIndexes"])

            if raw_data['UserExperiencesProcessAuto'] == True:
                raw_data['Experience'] = select_by_indexes(raw_data['Experience'], output["ExperienceIndexes"])

            if raw_data['UserStrengsProcessAuto'] == True:
                raw_data['Strengs'] = select_by_indexes(raw_data['Strengs'], output["StrengthIndexes"])

            if raw_data['UserSkillsProcessAuto'] == True:
                raw_data['Skills'] = select_by_indexes(raw_data['Skills'], output["SkillIndexes"])



            print("------------------------------\nCo wysyłam do generatora \n\n",raw_data)
            # Wybiernanie danych pod CV


            ## Mapowanie danych

            mapper = CvDataMapper()
            cv_document = mapper.map_from_dict(raw_data)
            print("------------------------------\nCo wysyłam do generatora \n\n", cv_document)
            print("cv_dokument \n")
            file_url = build_pdf(cv_document, raw_data["UserId"])
            UploadGeneratedCV(raw_data['UserId'],f"{file_url}",scrap_json['company_name'])
        except Exception as ex:
            raw_data = json.loads(massage)
            print("ERROR !!! \n\n\n",ex)
            EndProcessError(raw_data['UserId'])
