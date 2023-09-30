// Get references to DOM elements
var newPasswordInput = document.querySelector('.new-password-input');
var confirmPasswordInput = document.querySelector('.confirm-password-input');
var message = document.querySelector('.message');
var passError = document.querySelector('.passwordError');
const container = document.querySelector('.container');

// Function to toggle password visibility
// Function to toggle password visibility
const passToggle = (element) => {
    const passwordInput = element.previousElementSibling; // Get the input element before the clicked icon

    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        element.style.visibility = 'hidden';
        element.nextElementSibling.style.visibility = 'visible'; // Show the other eye icon
    } else {
        passwordInput.type = 'password';
        element.style.visibility = 'hidden';
        element.previousElementSibling.style.visibility = 'visible'; // Show the other eye icon
    }
};


function checkPassMatch() {
    if (newPasswordInput.value === confirmPasswordInput.value) {
        newPasswordInput.style.border = "1px solid green";
        confirmPasswordInput.style.border = "1px solid green";
        message.innerHTML = '<i class="fa-solid fa-circle-check"></i>Mật khẩu trùng khớp';
        canSubmit.value = true;
    } else {
        newPasswordInput.style.border = "1px solid red";
        confirmPasswordInput.style.border = "1px solid red";
        container.style.height = '370px';
        message.style.display = 'block';
        message.innerHTML = `<i class="fa-solid fa-circle-check"></i>Mật khẩu không trùng khớp`;
        canSubmit.value = false;
    }
}

function checkPasswordValid() {
    var check = true;
    var error = "";
    var checkRegrex = new RegExp(/^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%^&+*!=]).*$/);
    if (!checkRegrex.test(newPasswordInput.value)) {
        error = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt, một ký tự số, một ký tự in hoa và in thường.";
        check = false;
    }

    if (check === true) {
        newPasswordInput.style.border = "1px solid green";
        passError.innerHTML = "";
    } else {
        passError.innerHTML = error;
        newPasswordInput.style.border = "1px solid red";
    }
}

// Event listeners
document.querySelector('.fa-eye').addEventListener('click', passToggle);
document.querySelector('.fa-eye-slash').addEventListener('click', passToggle);
document.getElementById("reset-button").addEventListener("click", function (event) {
    if (document.getElementById("canSubmit").value === "false") {
        event.preventDefault();
        alert("Mật khẩu phải trùng khớp");
    }
});

