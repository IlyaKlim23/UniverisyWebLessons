document.getElementById('send-form').addEventListener('submit', submitForm);

function submitForm(event) {
    event.preventDefault();
    let formData = new FormData(event.target);

    let obj = {};
    formData.forEach((value, key) => obj[key] = value);

    let request = new Request(event.target.action, {
        method: 'POST',
        body: JSON.stringify(obj),
        headers: {
            'Content-Type': 'application/json',
        },
    });

    fetch(request).then(
        function(response) {
            console.log(response);
        },
        function(error) {
            console.error(error);
        }
    );

    console.log('Запрос отправляется');
}