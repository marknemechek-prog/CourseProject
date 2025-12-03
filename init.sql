-- Drop tables if they already exist (for re-run safety)
DROP TABLE IF EXISTS Actions;
DROP TABLE IF EXISTS Items;
DROP TABLE IF EXISTS Users;

-- ===========================
-- 1. USERS (викладачі)
-- ===========================
CREATE TABLE Users (
    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
    Name          TEXT    NOT NULL,
    Email         TEXT    NOT NULL UNIQUE,
    PasswordHash  TEXT    NOT NULL
);

-- ===========================
-- 2. ITEMS (курси)
-- ===========================
CREATE TABLE Items (
    Id          INTEGER PRIMARY KEY AUTOINCREMENT,
    Name        TEXT    NOT NULL,
    Description TEXT,
    Semester    INTEGER NOT NULL,
    UserId      INTEGER NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id)
);

-- ===========================
-- 3. ACTIONS (пропозиції по курсах)
-- ===========================
CREATE TABLE Actions (
    Id            INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId        INTEGER NOT NULL,
    ItemId        INTEGER NOT NULL,
    ActionType    TEXT    NOT NULL, -- 'Create' або 'Delete'
    ActionDetails TEXT,
    Status        TEXT    NOT NULL, -- 'Pending', 'Approved', 'Rejected'
    CreatedAt     TEXT    NOT NULL DEFAULT (datetime('now')),
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ItemId) REFERENCES Items(Id)
);

-- ===========================
-- Тестові дані
-- ===========================

-- Викладачі
INSERT INTO Users (Name, Email, PasswordHash) VALUES
('Іван Петров', 'ivan.petrov@university.edu', 'HASH1'),
('Олена Іваненко', 'olena.ivanenko@university.edu', 'HASH2');

-- Курси (Items) – привʼязані до викладачів через UserId
INSERT INTO Items (Name, Description, Semester, UserId) VALUES
('Програмування 1', 'Вступ до програмування мовою C#', 1, 1),
('Бази даних', 'Реляційні БД та SQL', 3, 1),
('Компʼютерні мережі', 'Основи побудови мереж', 4, 2);

-- Пропозиції (Actions)
INSERT INTO Actions (UserId, ItemId, ActionType, ActionDetails, Status) VALUES
(1, 1, 'Create', 'Створити новий курс для першого курсу', 'Approved'),
(1, 2, 'Delete', 'Обʼєднати курс БД з іншим, можна видалити', 'Pending'),
(2, 3, 'Create', 'Новий курс з мереж для 4-го семестру', 'Rejected');
