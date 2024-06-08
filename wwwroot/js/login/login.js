import { firebaseApp } from '../firebaseApp.js'
const auth = firebaseApp.auth();

async function Login() {
    const email = document.getElementById('emailLogin').value;
    const password = document.getElementById('passwordLogin').value;

    try {
        const userCredencial = await auth.signInWithEmailAndPassword(email, password)
        const user = userCredencial.user;
        alert(user.uid);
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
        this.submit();
    }
    else {
        alert("Usuario o Contraseña Incorrecto.");
    }
})