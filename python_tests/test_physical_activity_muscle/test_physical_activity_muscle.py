from urllib.parse import quote
import warnings
import pytest
import requests
import json

from urllib3.exceptions import InsecureRequestWarning

muscle = {
    "name": "pythonMuscle"
}

activity = {
    "name": "pythonActivity",
    "calories": 10
}


def test_get_muscle(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            get_response = api_client.get_muscles(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_muscles(None)
        assert get_response.status_code == 401



def test_post_muscle(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_muscle(admin_token, muscle)
            assert post_response.status_code == 200

        post_response = api_client.post_muscle(None, muscle)
        assert post_response.status_code == 401


def test_post_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 200

        post_response = api_client.post_physical_activity(None, activity)
        assert post_response.status_code == 401


def test_put_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            put_response = api_client.put_physical_activity_muscle(admin_token, activity["name"], muscle["name"])
            assert put_response.status_code == 200

        put_response = api_client.put_physical_activity_muscle(None, activity["name"], muscle["name"])
        assert put_response.status_code == 401


def test_delete_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_physical_activity_muscle(admin_token, 
                                                                            activity["name"], muscle["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_physical_activity_muscle(None, activity["name"], muscle["name"])
        assert delete_response.status_code == 401


def test_delete_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_physical_activity(admin_token, activity["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_physical_activity(None, activity["name"])
        assert delete_response.status_code == 401


def test_delete_muscle(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_muscle(admin_token, muscle["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_muscle(None, muscle["name"])
        assert delete_response.status_code == 401
