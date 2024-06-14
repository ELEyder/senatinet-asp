import { firebaseApp } from '../firebaseApp.js'
const auth = firebaseApp.auth();
const baseURL = window.location.origin;

async function Login() {
    const email = document.getElementById('emailLogin').value;
    const password = document.getElementById('passwordLogin').value;

    try {
        const userCredential = await auth.signInWithEmailAndPassword(email, password);
        const idToken = await userCredential.user.getIdToken();
        localStorage.setItem('firebaseToken', idToken);

        return postLogin();
    } catch (error) {
        console.error('Error de autenticación:', error);
        if (error.code === 'auth/invalid-email') {
            console.error('El formato del correo electrónico no es válido.');
            alert('El formato del correo electrónico no es válido.');
        } else if (error.code === 'auth/user-disabled') {
            console.error('La cuenta de usuario ha sido deshabilitada.');
            alert('La cuenta de usuario ha sido deshabilitada.');
        } else if (error.code === 'auth/user-not-found') {
            console.error('No se encontró un usuario con este correo electrónico.');
            alert('No se encontró un usuario con este correo electrónico.');
        } else if (error.code === 'auth/wrong-password') {
            console.error('La contraseña es incorrecta.');
            alert('La contraseña es incorrecta.');
        } else if (error.code === 'auth/invalid-credential') {
            console.error('La credencial proporcionada es incorrecta, mal formada o ha expirado.');
            alert('La credencial proporcionada es incorrecta, mal formada o ha expirado.');
            alert('Pruebe nuestros proovedores o registre una cuenta.');

        } else {
            console.error('Error desconocido:', error.message);
            alert('Error desconocido:', error.message);
        }
        return false;
    }
}
document.getElementById('formLogin').addEventListener("submit", async function (event) {
    event.preventDefault();

    changeLogin();

    const request = await Login();

    if (request) {
        alert("Login realizado.");
        window.location.replace(baseURL + '/home');
    }
    else {
        alert("No se ha completado el logueo");
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

async function postLogin() {
    const idToken = localStorage.getItem('firebaseToken')
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
}