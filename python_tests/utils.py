import warnings
from urllib3.exceptions import InsecureRequestWarning

def register_user(api_client, user):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        response = api_client.register(user)
        return response

def login_user(api_client, user):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        response = api_client.login(user)
        return response

def delete_user(api_client, email, admin_token):
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        response = api_client.delete_user_by_email(email, admin_token)
        return response