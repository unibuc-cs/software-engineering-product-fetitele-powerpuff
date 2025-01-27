import warnings
from urllib3.exceptions import InsecureRequestWarning


tutorial = {
    "title": "pythonTutorial",
    "grams": 400,
    "calories": 200,
    "carbohydrates": 20,
    "proteins": 10,
    "fats": 5,
    "videolink": "link"
}


def test_get_tutorial(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_tutorial(user_token)
            assert get_response.status_code == 200

        get_response = api_client.get_tutorial(None)
        assert get_response.status_code == 401


def test_post_and_delete_tutorial(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_tutorial(admin_token, tutorial)
            assert post_response.status_code == 200

            delete_response = api_client.delete_tutorial(admin_token, tutorial["title"])
            assert delete_response.status_code == 200

        post_response = api_client.post_tutorial(None, tutorial)
        assert post_response.status_code == 401