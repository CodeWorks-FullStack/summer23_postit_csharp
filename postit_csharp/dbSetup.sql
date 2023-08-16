CREATE TABLE
    accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE 
    albums(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(255) NOT NULL,
        category ENUM('misc', 'cats', 'dogs', 'games', 'gachamon', 'animals') DEFAULT 'misc',
        archived BOOLEAN DEFAULT FALSE,
        coverImg VARCHAR(700) NOT NULL,
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY(creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

DROP TABLE albums;

INSERT INTO albums (title, category, coverImg, creatorId)
VALUES ('hot dogs', 'dogs','https://images.unsplash.com/photo-1541214113241-21578d2d9b62?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1170&q=80',
'64dcedd3c5d6acdbaa571baa'
    );

SELECT 
alb.*, 
acc.*
FROM albums alb
JOIN accounts acc
ON acc.id = alb.creatorId;

SELECT 
    alb.*,
    acc.* 
    FROM albums alb
    JOIN accounts acc ON acc.id = alb.creatorId 
    WHERE alb.id = 2 LIMIT 1;

UPDATE albums SET archived = true WHERE id = 1;