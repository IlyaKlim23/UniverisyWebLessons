document.getElementById('send-form').addEventListener('submit', submitForm);

function submitForm(event) {
    event.preventDefault();
    let formData = new FormData(event.target);

    let obj = {};
    formData.forEach((value, key) => obj[key] = value);

    let response = fetch(event.target.action, {
        method: 'POST',
        body: JSON.stringify(obj),
        headers: {
            'Content-Type': 'application/json',
        },
    });

    alert("Успешно отправлено");

    // Swal.fire(
    //     'Успешно',
    //     'Сообщение отправлено!',
    //     'success'
    // )

    console.log('Запрос отправляется');
}