import { firebaseApp } from '../firebaseApp.js'
const auth = firebaseApp.auth();

async function SingUp(email, password) {
    try {
        const userCredencial = await auth.createUserWithEmailAndPassword(email, password)
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
document.getElementById('formSingUp').addEventListener("submit", async function (event) {
    event.preventDefault();
    const firstName = document.getElementById('firstNameSingUp').value;
    const lastName = document.getElementById('lastNameSingUp').value;
    const email = document.getElementById('emailSingUp').value;
    const password1 = document.getElementById('passwordSingUp1').value;
    const password2 = document.getElementById('passwordSingUp2').value;

    const passGeneric = ['12345678', 'abc', 'password'];
    if (password1 != password2)
        return alert("Las contraseñas deben ser iguales.");
    if (passGeneric.includes(password1))
        return alert("La contraseña no puede ser tan genérica.");

    const request = await SingUp(email, password1);
    alert(request)
    if (request == true) {
        alert("Registro realizado.");
        /*this.submit();*/
    }
    else {
        alert("Algo a fallado en el registro, revise la consola del navegador");
    }
})