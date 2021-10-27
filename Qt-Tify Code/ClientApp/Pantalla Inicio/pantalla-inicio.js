//#region navbar
const menu = document.querySelector("#mobile__menu");
const menuLinks = document.querySelector(".navbar__ul");

menu.addEventListener("click", function () {
  menu.classList.toggle("is-active");
  menuLinks.classList.toggle("active");
});
//#endregion

const fila = document.querySelector('.carousel__contenedor');
const albumes = document.querySelector('.album');

const flechaIzquierda = document.getElementById('flecha-izquierda');
const flechaDerecha = document.getElementById('flecha-derecha');

/* flecha derecha */

flechaDerecha.addEventListener('click', () => {
fila.scrollLeft += fila.offsetWidth;
});

/* flecha izquierda */

flechaIzquierda.addEventListener('click', () => {
	fila.scrollLeft -= fila.offsetWidth;  })

  /* hover */ 

  albumes.forEach((album) => {
    album.addEventListener('mouseenter', (e) => {
      const elemento = e.currentTarget;
      setTimeout(() => {
        albumes.forEach(album => album.classList.remove('hover'));
        elemento.classList.add('hover');
      }, 300);
    });
  });
  
  fila.addEventListener('mouseleave', () => {
    albumes.forEach(albumes => album.classList.remove('hover'));
  });