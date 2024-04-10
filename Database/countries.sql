CREATE TABLE Countries (
    code TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    continent TEXT,
    description TEXT
);

CREATE TABLE Data (
    id INT PRIMARY KEY,
    name TEXT NOT NULL,
    country TEXT NOT NULL,
    description TEXT,
    FOREIGN KEY (country) REFERENCES Countries (code) ON UPDATE CASCADE ON DELETE CASCADE,
    UNIQUE (name, country)
);

CREATE TABLE DataPoints (
    id INT PRIMARY KEY,
    data_id INT NOT NULL,
    key TEXT NOT NULL,
    value FLOAT NOT NULL,
    UNIQUE (data_id, key),
    FOREIGN KEY (data_id) REFERENCES Data (id) ON UPDATE CASCADE ON DELETE CASCADE
);
