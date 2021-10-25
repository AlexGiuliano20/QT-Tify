const nav = document.querySelector('.navbar__ul');
document.getElementById("icon").onclick = function() {toggleActive()};

function toggleActive(){
    nav.classList.toggle('navbar__ul-active');
}