import warnings
from urllib3.exceptions import InsecureRequestWarning


profile = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": 60,
  "height": 175,
  "goal": 0
}

profile_wrong_date = {
  "name": "dummy name",
  "birthdate": "2104-05-18",
  "weight": 60,
  "height": 175,
  "goal": 0
}

profile_wrong_weight = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": -60,
  "height": 175,
  "goal": 0
}

put_profile = {
  "name": "dummy name",
  "birthdate": "2004-05-18",
  "weight": 62,
  "height": 175,
  "goal": 0
}


def test_get_all_profiles(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if admin_token is not None:
            get_response = api_client.get_all_profiles(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_all_profiles(None)
        assert get_response.status_code == 401


def test_post_user_profile_wrong(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile_wrong_date)
            assert post_response.status_code == 400

            post_response = api_client.post_user_profile(user_token, profile_wrong_weight)
            assert post_response.status_code == 400


def test_post_user_profile(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            delete_response = api_client.delete_user_profile(user_token)
            assert delete_response.status_code == 200

            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 200

            # Clean up
            delete_response = api_client.delete_user_profile(user_token)
            assert delete_response.status_code == 200

        post_response = api_client.post_user_profile(None, profile)
        assert post_response.status_code == 401


def test_post_user_profile_twice(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 200

            post_response = api_client.post_user_profile(user_token, profile)
            assert post_response.status_code == 400


def test_put_user_profile(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            put_response = api_client.put_user_profile(user_token, put_profile)
            assert put_response.status_code == 200

        put_response = api_client.put_user_profile(None, put_profile)
        assert put_response.status_code == 401


def test_get_user_profile(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            get_response = api_client.get_user_profile(user_token)
            assert get_response.status_code == 200

        get_response = api_client.get_user_profile(None)
        assert get_response.status_code == 401


def test_delete_user_profile(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)
    
        if user_token is not None:
            delete_response = api_client.delete_user_profile(user_token)
            assert delete_response.status_code == 200

        delete_response = api_client.delete_user_profile(None)
        assert delete_response.status_code == 401
