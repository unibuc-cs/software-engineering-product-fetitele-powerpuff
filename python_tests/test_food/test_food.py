import warnings
from urllib3.exceptions import InsecureRequestWarning

food = {
    "name": "pythonFood",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

food_invalid_calories = {
    "name": "pythonFood2",
    "calories": -1,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

food_invalid_carbs = {
    "name": "pythonFood3",
    "calories": 100,
    "carbohydrates": -1,
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


def test_post_food(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        post_response = api_client.post_food(None, food)
        assert post_response.status_code == 401

        if admin_token is not None:
            post_response = api_client.post_food(admin_token, food)
            assert post_response.status_code == 200

            post_response = api_client.post_food(admin_token, food)
            assert post_response.status_code == 400

            post_response = api_client.post_food(admin_token, food_invalid_calories)
            assert post_response.status_code == 400

            post_response = api_client.post_food(admin_token, food_invalid_carbs)
            assert post_response.status_code == 400


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


def test_delete_food(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        delete_response = api_client.delete_food(None, food['name'])
        assert delete_response.status_code == 401

        if admin_token is not None:
            delete_response = api_client.delete_food(admin_token, food["name"])
            assert delete_response.status_code == 200

            delete_response = api_client.delete_food(admin_token, food["name"])
            assert delete_response.status_code == 404