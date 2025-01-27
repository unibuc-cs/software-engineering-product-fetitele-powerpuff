import warnings
from urllib3.exceptions import InsecureRequestWarning

activity = {
    "name": "pythonActivity",
    "calories": 10
}


def test_get_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_physical_activity(user_token)
            assert get_response.status_code == 200
    
        if admin_token is not None:
            get_response = api_client.get_physical_activity_admin(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_physical_activity(None)
        assert get_response.status_code == 401
        
        get_response = api_client.get_physical_activity_admin(None)
        assert get_response.status_code == 401


def test_post_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 200

        post_response = api_client.post_physical_activity(None, activity)
        assert post_response.status_code == 401


def test_post_physical_activity_twice(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_physical_activity(admin_token, activity)
            assert post_response.status_code == 400

        post_response = api_client.post_physical_activity(None, activity)
        assert post_response.status_code == 401


def test_get_physical_activity_name(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_physical_activity_name(user_token, activity["name"])
            assert get_response.status_code == 200
    
            get_response = api_client.get_physical_activity_name(user_token, "wrong_name")
            assert get_response.status_code == 404

        get_response = api_client.get_physical_activity_name(None, activity["name"])
        assert get_response.status_code == 401


def test_delete_physical_activity(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            delete_response = api_client.delete_physical_activity(admin_token, activity["name"])
            assert delete_response.status_code == 200

        delete_response = api_client.delete_physical_activity(None, activity["name"])
        assert delete_response.status_code == 401
