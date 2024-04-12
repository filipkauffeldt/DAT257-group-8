CREATE TABLE Countries (
    code TEXT PRIMARY KEY,
    name TEXT NOT NULL,
    continent TEXT,
    description TEXT
);

CREATE TABLE Data (
    id BIGSERIAL PRIMARY KEY,
    name TEXT NOT NULL,
    country TEXT NOT NULL,
    description TEXT,
    unit TEXT NOT NULL,
    FOREIGN KEY (country) REFERENCES Countries (code) ON UPDATE CASCADE ON DELETE CASCADE,
    UNIQUE (name, country)
);

CREATE TABLE DataPoints (
    id BIGSERIAL PRIMARY KEY,
    data_id INT NOT NULL,
    date DATE NOT NULL,
    value FLOAT NOT NULL,
    UNIQUE (data_id, date),
    FOREIGN KEY (data_id) REFERENCES Data (id) ON UPDATE CASCADE ON DELETE CASCADE
);
