"use struct"

document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('send-form');
    form.addEventListener('submit', formSend);

    async function formSend(e){
        e.preventDefault();
        const response = await fetch("/Program.cs", {
            method: "POST",
            body: JSON.stringify({
                firstName: document.getElementById("first-name").value,
                secondName: document.getElementById("second-name").value,
                birthDate: document.getElementById("birth-date").value,
                phoneNumber: document.getElementById("phone-number").value,
                email: document.getElementById("mail").value
            })
        });
        const message = await response.json();
        console.log(message)
    }

    function formValidate(form){
        let error = 0;
        let formReq = document.querySelectorAll('._req');

        for (let index = 0; index < formReq.length; index++){
            const input = formReq[index];
            formRemoveError(input);
            if (input.value === '') {
                formAddError(input);
                error++;
            }
        }
        return (error);
    }

    function formAddError(input){
        input.classList.add('_error');
    }
    function formRemoveError(input){
        input.classList.remove('_error');
    }
});