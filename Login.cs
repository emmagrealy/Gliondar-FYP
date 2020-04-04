using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Text.RegularExpressions;

public class Login : MonoBehaviour {
	public GameObject email;
	public GameObject password;
	private string Email;
	private string Password;

	public void LoginButton()
    {
        bool EM = false;
		bool PW = false;
		bool CPW = false;

		Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

		if (Email != "")
        {
				if(Email.Contains("@")){
					if(Email.Contains(".")){
						EM = true;
					} else {
						Debug.LogWarning("Email is Incorrect");
					}
				} else {
					Debug.LogWarning("Email is Incorrect");
				}
		} else {
			Debug.LogWarning("Email Field Empty");
		}
		if (Password != ""){
			if(Password.Length > 5){
				PW = true;
			} else {
				Debug.LogWarning("Password Must Be atleast 6 Characters long");
			}
		} else {
			Debug.LogWarning("Password Field Empty");
		}
		
		if (EM == true&&PW == true)
        {
            Firebase.Auth.Credential credential =
            Firebase.Auth.EmailAuthProvider.GetCredential(Email, Password);
            auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
            if (task.IsCanceled) 
            {
                Debug.LogError("SignInWithCredentialAsync was canceled.");
                return;
            }

            if (task.IsFaulted) 
            {
                Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
                return;
            }
            });
		
	// Update is called once per frame
	void Update () {
			if (email.GetComponent<InputField>().isFocused){
				password.GetComponent<InputField>().Select();
			}
			if (Password != ""&&Password != ""){
				LoginButton();
			}
		Email = email.GetComponent<InputField>().text;
		Password = password.GetComponent<InputField>().text;	
	}
}
}
}


