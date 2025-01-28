import warnings
from urllib3.exceptions import InsecureRequestWarning

article = {
    "title": "pythonArticle",
    "author": "pythonAuthor",
    "content": "This is a python article"
}


def test_get_article(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if user_token is not None:
            get_response = api_client.get_article(user_token)
            assert get_response.status_code == 200

        get_response = api_client.get_article(None)
        assert get_response.status_code == 401


def test_post_and_delete_article(api_client, setup_and_teardown):
    user_token, admin_token, user, admin = setup_and_teardown
    with warnings.catch_warnings():
        warnings.simplefilter("ignore", InsecureRequestWarning)

        if admin_token is not None:
            post_response = api_client.post_article(admin_token, article)
            assert post_response.status_code == 200

            delete_response = api_client.delete_article(admin_token, article["title"])
            assert delete_response.status_code == 200

        post_response = api_client.post_article(None, article)
        assert post_response.status_code == 401