from Tools.PDF.Cv_Templates.CvModel import CvModel


class sotrage():
    def __init__(self,UserId: str, OfferURL: str, UserData: dict  ):

        self.userId = UserId
        self.offerURL = OfferURL
        self.Data =  UserData
        self.offerURLResponse: str = ""
        self.massage: dict = {}

        self.Cv : CvModel = CvModel()
