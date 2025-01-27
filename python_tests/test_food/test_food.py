from urllib.parse import quote
import warnings
import pytest
import requests
import json

from urllib3.exceptions import InsecureRequestWarning

food = {
    "name": "pythonFood",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}


def test_get_food(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_food(user_token)
            assert get_response.status_code == 200
    
        if admin_token is not None:
            get_response = api_client.get_food_admin(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_food(None)
        assert get_response.status_code == 401


def test_get_food_name(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_food_name(user_token, food["name"])
            assert get_response.status_code == 200
    
            get_response = api_client.get_food_name(user_token, "wrong_name")
            assert get_response.status_code == 404

        get_response = api_client.get_food_name(None, food["name"])
        assert get_response.status_code == 401
