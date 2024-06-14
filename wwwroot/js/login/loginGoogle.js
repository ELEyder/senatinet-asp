import { firebaseApp } from '../firebaseApp.js'
const auth = firebaseApp.auth();
const baseURL = window.location.origin;

async function Login() {
    try {
        const provider = new firebase.auth.GoogleAuthProvider();
        let idToken;
            const result = await auth.signInWithPopup(provider);
            //const result = await auth.signInWithRedirect(provider);
            console.log(result.user);
            idToken = await result.user.getIdToken();
        localStorage.setItem('firebaseToken', idToken);

        // Enviar el ID token al backend
        const response = await fetch(baseURL + '/login/authenticate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + idToken
            }
        });
        if (!response.ok) {
            throw new Error('Error en la respuesta del servidor');
        }

        return true;
    } catch (error){
        const errorCode = error.code;
        const errorMessage = error.message;
        console.log(errorCode);
        console.log(errorMessage);
        console.log(error);
        return false;
    }
}
document.getElementById('btn-google').addEventListener("click", async function () {
    changeLogin();

    const request = await Login();
    console.log(request);
    if (request == true) {
        alert("Login realizado.");
        window.location.replace(baseURL + '/home');
    }
    else {
        alert("Usuario o contraseña Incorrecto.");
    }
    changeLogin();
})

function changeLogin() {
    const button = document.getElementById('submitLogin');
    if (button.disabled) {
        button.disabled = false;
        button.classList.add('enabled');
        button.classList.remove('disabled');
    } else {
        button.disabled = true;
        button.classList.add('disabled');
        button.classList.remove('enabled');
    }
}