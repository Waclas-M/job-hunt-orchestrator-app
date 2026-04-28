from fastapi import FastAPI, HTTPException
from pydantic import BaseModel, HttpUrl
from selenium import webdriver
from selenium.common.exceptions import WebDriverException, TimeoutException
from selenium.webdriver.common.by import By
import traceback
import time

app = FastAPI(title="Local Browser Fetch API")


class FetchRequest(BaseModel):
    url: HttpUrl
    wait_seconds: int = 5


class FetchResponse(BaseModel):
    success: bool
    Job_title :str | None = None
    CompanyName :str | None = None
    Localization: str | None = None
    Technology : list[str] | None = None
    responsibilites : list[str] | None = None
    requirements : list[str] | None = None
    error: str | None = None





@app.get("/health")
def health():
    return {"status": "ok"}


@app.post("/fetch", response_model=FetchResponse)
def fetch_page(payload: FetchRequest):
    driver = None
    try:
        driver = webdriver.Edge()
        driver.implicitly_wait(10)
        driver.get(str(payload.url))

        # offer-details
        # data-scroll-id = job-title
        # data-scroll-id = employer-name
        # data-scroll-id="technologies-1"
        # data-scroll-id="responsibilities-1"
        # data-scroll-id="requirements-1"


        Job_title = driver.find_element(By.CSS_SELECTOR, '[data-scroll-id="job-title"]').text
        CompanyName = driver.find_element(By.CSS_SELECTOR, '[data-test="text-employerName"]').text
        localization = driver.find_element(By.CSS_SELECTOR, '[data-test="sections-benefit-workplaces"]').find_element(By.CSS_SELECTOR, '[data-test="offer-badge-title"]').text

        # Technology
        Technology = []
        sectionsTechnology = driver.find_element(
            By.CSS_SELECTOR,
            '[data-test="section-technologies-expected"]'
        )
        lisTech = sectionsTechnology.find_elements(By.XPATH, ".//li")
        for li in lisTech:
            span = li.find_element(By.TAG_NAME, "span")
            Technology.append(span.get_attribute("textContent"))


        #Responsibilites
        responsibilities = []
        sectionResponsibilities = driver.find_element(By.CSS_SELECTOR,'[data-test="section-responsibilities"]')
        lisRes = sectionResponsibilities.find_elements(By.XPATH, ".//li")
        for li in lisRes:
            responsibilities.append(li.get_attribute("textContent"))



       # Requirements
        sectionRequirements = driver.find_element(By.CSS_SELECTOR,'[data-test="section-requirements"]')
        requirements = []
        lisReq = sectionRequirements.find_elements(By.XPATH, ".//li")
        for li in lisReq:
            requirements.append(li.get_attribute("textContent"))

        return FetchResponse(
            success=True,
            Job_title= str(Job_title),
            CompanyName= str(CompanyName),
            Localization= localization,
            Technology= Technology,
            requirements= requirements,
            responsibilites= responsibilities

        )


    except (TimeoutException, WebDriverException) as e:
        return FetchResponse(
            success=False,
            error=f"{type(e).__name__}: {str(e)}"
        )
    except Exception as e:
        return FetchResponse(
            success=False,
            error=f"{type(e).__name__}: {str(e)}\n{traceback.format_exc()}"
        )
    finally:
        if driver:
            driver.quit()