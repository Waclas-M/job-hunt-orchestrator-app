
from selenium import webdriver
from selenium.webdriver.common.by import By
import time
from Tests.modelApitest import ApiTest


from selenium.webdriver.edge.options import Options
from selenium.webdriver.edge.service import Service

service = Service(log_output="msedge.log")
options = Options()
options.use_chromium = True


options.add_argument("--no-sandbox")
options.add_argument("--disable-dev-shm-usage")
options.add_argument("--disable-gpu")
options.add_argument("--window-size=1920,1080")




def ScrapOffer(url):

    driver = webdriver.Edge(service=service,options=options)
    driver.implicitly_wait(10)
    driver.get(url)
    time.sleep(3)

    # offer-details
    # data-scroll-id = job-title
    # data-scroll-id = employer-name
    # data-scroll-id="technologies-1"
    # data-scroll-id="responsibilities-1"
    # data-scroll-id="requirements-1"
    print(driver.page_source)

    Job_title = driver.find_element(By.CSS_SELECTOR,'[data-scroll-id="job-title"]')
    CompanyName = driver.find_element(By.CSS_SELECTOR,'[data-scroll-id="employer-name"]')
    Technology = driver.find_element(By.CSS_SELECTOR,'[data-scroll-id="technologies-1"]')
    responsibilites = driver.find_element(By.CSS_SELECTOR,'[data-scroll-id="responsibilities-1"]')
    requirements = driver.find_element(By.CSS_SELECTOR,'[data-scroll-id="requirements-1"]')

    outer_html = Job_title.get_attribute("outerHTML")
    inner_html = Job_title.get_attribute("innerHTML")

    print("Job_title:")
    print(Job_title.get_attribute("innerHTML"))

    print("\nCompanyName:")
    print(CompanyName.get_attribute("innerHTML"))

    print("\nTechnology:")
    print(Technology.get_attribute("innerHTML"))


    print("\nresponsibilites:")
    print(responsibilites.get_attribute("innerHTML"))


    print("\nrequirements:")
    print(requirements.get_attribute("innerHTML"))

    driver.quit()

    output = ApiTest(f"""Z podanego niżej html wyciągnij i wypisz tylko tekst
        Wyciągnij nazwę firmy {CompanyName.get_attribute("innerHTML")} \n
        Wyciągnij zawrte technologię {Technology.get_attribute("innerHTML")} \n
        Wyciągnij zadania na stanowisku {responsibilites.get_attribute("innerHTML")} \n
        Wyciągnij wymagania na stanowisko {requirements.get_attribute("innerHTML")} \n

        """)
    return  output




#
#ScrapOffer('https://www.pracuj.pl/praca/net-developer-krakow-osiedle-centrum-e-12,oferta,1004653481?sug=sg_latest_bd_2')



