import warnings
from urllib3.exceptions import InsecureRequestWarning

food = {
    "name": "pythonFood2",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

food2 = {
    "name": "pythonFood3",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

admin_food = {
    "name": "pythonFood",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

accepted_request_id = None
denied_request_id = None


def test_post_request(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None and user_token is not None:
            post_response = api_client.post_food(user_token, food)
            assert post_response.status_code == 200

            post_response = api_client.post_food(user_token, food2)
            assert post_response.status_code == 200

            post_response = api_client.post_food(admin_token, admin_food)
            assert post_response.status_code == 200

            post_response = api_client.post_request(user_token, food['name'])
            assert post_response.status_code == 200

            post_response = api_client.post_request(user_token, food2['name'])
            assert post_response.status_code == 200

            post_response = api_client.post_request(user_token, food2['name'])
            assert post_response.status_code == 400

            post_response = api_client.post_request(user_token, 'wrong_name')
            assert post_response.status_code == 404

            post_response = api_client.post_request(user_token, admin_food['name'])
            assert post_response.status_code == 400

            post_response = api_client.post_request(admin_token, food['name'])
            assert post_response.status_code == 400

            delete_response = api_client.delete_food(admin_token, admin_food['name'])
            assert delete_response.status_code == 200

        post_response = api_client.post_request(None, food['name'])
        assert post_response.status_code == 401


def test_get_requests(api_client, setup_and_teardown):
    global accepted_request_id, denied_request_id

    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            get_response = api_client.get_requests(admin_token)
            assert get_response.status_code == 200

            request_data = get_response.json()

            get_response = api_client.get_food_admin(admin_token)
            assert get_response.status_code == 200

            food_data = get_response.json()

            for f in food_data:
                if f['name'] == food['name']:
                    food_id = f['id']

                if f['name'] == food2['name']:
                    food2_id = f['id']

            if food_id and food2_id:
                for request in request_data:
                    if request['foodId'] == food_id:
                        accepted_request_id = request['id']

                    elif request['foodId'] == food2_id:
                        denied_request_id = request['id']

        get_response = api_client.get_requests(None)
        assert get_response.status_code == 401


def test_get_request(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None and accepted_request_id is not None:
            get_response = api_client.get_request(admin_token, str(accepted_request_id))
            assert get_response.status_code == 200

            get_response = api_client.get_request(admin_token, '-1')
            assert get_response.status_code == 404

            get_response = api_client.get_request(None, str(accepted_request_id))
            assert get_response.status_code == 401


def test_put_and_delete_request(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            if accepted_request_id is not None:
                put_response = api_client.put_request(admin_token, str(accepted_request_id))
                assert put_response.status_code == 200

                put_response = api_client.put_request(admin_token, str(accepted_request_id))
                assert put_response.status_code == 404

                delete_response = api_client.delete_food(admin_token, food['name'])
                assert delete_response.status_code == 200

            if denied_request_id is not None:
                delete_response = api_client.delete_request(admin_token, str(denied_request_id))
                assert delete_response.status_code == 200

                delete_response = api_client.delete_request(admin_token, str(denied_request_id))
                assert delete_response.status_code == 404

            put_response = api_client.put_request(admin_token, '-1')
            assert put_response.status_code == 404

            delete_response = api_client.delete_request(admin_token, '-1')
            assert delete_response.status_code == 404

        put_response = api_client.put_request(None, '-1')
        assert put_response.status_code == 401

        delete_response = api_client.delete_request(None, '-1')
        assert delete_response.status_code == 401
