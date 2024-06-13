import { firebaseApp } from '../firebaseApp.js'
const auth = firebaseApp.auth();
const baseURL = window.location.origin;

async function Login() {
    const email = document.getElementById('emailLogin').value;
    const password = document.getElementById('passwordLogin').value;

    try {
        const userCredential = await auth.signInWithEmailAndPassword(email, password)
        const idToken = await userCredential.user.getIdToken();
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
        return false;
    }
}
document.getElementById('formLogin').addEventListener("submit", async function (event) {
    event.preventDefault();

    const request = await Login();
    alert(request)
    if (request == true) {
        alert("Login realizado.");
        window.location.replace(baseURL + '/home');
    }
    else {
        alert("Usuario o contraseña Incorrecto.");
    }
})