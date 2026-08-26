Create database portalargentina;

USE portalargentina;

CREATE TABLE usuarios
(
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    senha VARCHAR(255) NOT NULL,
    administrador BOOLEAN DEFAULT FALSE,
    pontosQuiz INT DEFAULT 0,
    dataCadastro DATETIME DEFAULT CURRENT_TIMESTAMP
);

Select * from usuarios;