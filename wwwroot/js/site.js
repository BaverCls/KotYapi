console.log("Kot Yapı");
document.querySelectorAll('.project-card-hover').forEach(card => {
    card.addEventListener('mouseenter', () => {
        card.querySelector('.project-info').classList.remove('d-none');
    });
    card.addEventListener('mouseleave', () => {
        card.querySelector('.project-info').classList.add('d-none');
    });
});