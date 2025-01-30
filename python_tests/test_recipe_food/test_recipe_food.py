import warnings
from urllib3.exceptions import InsecureRequestWarning

food = {
    "name": "pythonFood",
    "calories": 100,
    "carbohydrates": 14,
    "proteins": 11,
    "fats": 7
}

recipe = {
    "name": "pythonRecipe",
    "description": "This is a python recipe"
}


def test_post_and_delete_recipe_food(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_recipe(admin_token, recipe)
            assert post_response.status_code == 200

            post_response = api_client.post_food(admin_token, food)
            assert post_response.status_code == 200

            post_response = api_client.post_recipe_food(admin_token, recipe["name"], food["name"], 300)
            assert post_response.status_code == 200

            delete_response = api_client.delete_recipe_food(admin_token, recipe["name"], food["name"])
            assert delete_response.status_code == 200

            delete_response = api_client.delete_recipe(admin_token, recipe["name"])
            assert delete_response.status_code == 200

        post_response = api_client.post_recipe_food(None, recipe["name"], food["name"], 300)
        assert post_response.status_code == 401

        delete_response = api_client.delete_food(admin_token, food["name"])
        assert delete_response.status_code == 200