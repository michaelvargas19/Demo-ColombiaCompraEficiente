var production = true;
var nameEnvironment = 'local';
var ID_APPLICACION = "Manager";
var ORGANIZATION = "Colombia Compra Segura";
var GENERIC_TOKEN = "sad";
var URL_LOGIN = '';
var URL_CATALOGOS = '';
var URL_ORDENES = '';
var KEY_ENCRIPT = "";
var timeInactividad = 900000


switch (nameEnvironment) {
  case 'local':
    URL_LOGIN = "https://localhost:44376";
    URL_CATALOGOS = "https://localhost:44383";
    URL_ORDENES = "https://localhost:44311";
    break;

}

export const environment = {
  production: production,
  ID_APPLICACION: ID_APPLICACION,
  ORGANIZATION: ORGANIZATION,
  GENERIC_TOKEN: GENERIC_TOKEN,
  URL_LOGIN: URL_LOGIN,
  URL_CATALOGOS: URL_CATALOGOS,
  URL_ORDENES: URL_ORDENES,
  keyEncript: KEY_ENCRIPT,
  timeInactividad: timeInactividad
};