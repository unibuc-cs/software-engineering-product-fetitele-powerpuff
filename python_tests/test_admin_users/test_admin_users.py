import warnings
from urllib3.exceptions import InsecureRequestWarning


dummy_user2 = {
    "email": "dummy2@example.com",
    "password": "Dummy2pa55!",
    "confirmPassword": "Dummy2pa55!"
}

mismatched_user = {
    "email": "dummy3@example.com",
    "password": "Dummy3pa55!",
    "confirmPassword": "Dummypa55!"
}

admin_wrong = {
    "email": "admin1@example.com",
    "password": "Admin1pa555!" 
}


def test_register(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        
        # Register user
        register_response = api_client.register(dummy_user2)
        assert register_response.status_code == 200

        # Clean up
        delete_response = api_client.delete_user_by_email(dummy_user2["email"], admin_token)
        assert delete_response.status_code == 200


def test_register_same_email(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
        
        register_response = api_client.register(user)
        assert register_response.status_code == 500


def test_register_different_passwords(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        register_response = api_client.register(mismatched_user)
        assert register_response.status_code == 400


def test_login_admin_wrong(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        # Login admin
        login_response = api_client.login(admin_wrong)
        assert login_response.status_code == 401


def test_admin_workflow(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        # Login admin
        login_response = api_client.login(admin)
        assert login_response.status_code == 200
        token = login_response.json()['token']

        users_response = api_client.get_all_users(token)
        assert users_response.status_code == 200
        users = users_response.json()

        register_response = api_client.register(dummy_user2)
        assert register_response.status_code == 200

        promote_response = api_client.promote(dummy_user2["email"], token)
        assert promote_response.status_code == 200

        delete_email_response = api_client.delete_user_by_email(dummy_user2["email"], token)
        assert delete_email_response.status_code == 200