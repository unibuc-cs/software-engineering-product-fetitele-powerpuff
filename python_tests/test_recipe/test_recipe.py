import warnings
from urllib3.exceptions import InsecureRequestWarning

recipe = {
    "name": "pythonRecipe",
    "description": "This is a python recipe"
}


def test_get_recipe(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_recipe(user_token)
            assert get_response.status_code == 200

        if admin_token is not None:
            get_response = api_client.get_recipe_admin(admin_token)
            assert get_response.status_code == 200

        get_response = api_client.get_recipe(None)
        assert get_response.status_code == 401


def test_post_and_delete_recipe(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_recipe(admin_token, recipe)
            assert post_response.status_code == 200

            delete_response = api_client.delete_recipe(admin_token, recipe["name"])
            assert delete_response.status_code == 200

        post_response = api_client.post_recipe(None, recipe)
        assert post_response.status_code == 401