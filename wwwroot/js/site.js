// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener('DOMContentLoaded', function () {
    const form = document.querySelector('form');
    const password = document.getElementById('password');
    const confirmPassword = document.getElementById('confirmPassword');
    const confirmPasswordError = document.getElementById('confirmPasswordError');

    form.addEventListener('submit', function (event) {
        if (password.value !== confirmPassword.value) {
            event.preventDefault();
            confirmPasswordError.textContent = "Passwords do not match.";
        } else {
            confirmPasswordError.textContent = "";
        }
    });
});