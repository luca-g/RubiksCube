$(function () {
    $(document).on('click', '.parameter-button', (e) => {
        e.preventDefault();
        var parameterValue = $(e.currentTarget).data('value');        
        window.location.href = '/' + parameterValue;
    });
});