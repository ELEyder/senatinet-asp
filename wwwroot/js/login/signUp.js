import { firebaseApp } from '../firebaseApp.js'
const db = firebaseApp.firestore();
document.getElementById('formSignUp').addEventListener("submit", async function (event) {
    event.preventDefault();
    changeSignUp();
    //Data
    const username = document.getElementById('usernameSignUp').value;
    const firstName = document.getElementById('firstNameSignUp').value;
    const lastName = document.getElementById('lastNameSignUp').value;
    const email = document.getElementById('emailSignUp').value;
    const password1 = document.getElementById('passwordSignUp1').value;
    const password2 = document.getElementById('passwordSignUp2').value;
    //Passwords
    const passGeneric = ['12345678', 'abc', 'password'];

    console.log(username)

    if (username.includes('@')) {
        changeSignUp();
        return alert("No puedes tener arrobas en el nombre de usuario")
    }
    if (password1 != password2) {
        changeSignUp();
        return alert("Las contraseñas deben ser iguales.");
    }
    if (passGeneric.includes(password1)) {
        changeSignUp();
        return alert("La contraseña no puede ser tan genérica.");
    }

    //Query
    const query = await db.collection('users').where('username', '==', username).get();
    const queryEmail = await db.collection('users').where('email', '==', email).get();

    if (!query.empty) {
        changeSignUp();
        return alert("El usuario ya existe");
    }
    if (!queryEmail.empty) {
        changeSignUp();
        return alert("Correo ya registrado");
    }
    alert("Registro realizado.");
    //Enabled ButtonSignUp
    changeSignUp();
    this.submit();
})

function changeSignUp() {
    const button = document.getElementById('submitSignUp');
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