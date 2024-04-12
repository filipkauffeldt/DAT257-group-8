--
-- NOTE:
--
-- File paths need to be edited. Search for $$PATH$$ and
-- replace it with the path to the directory containing
-- the extracted data files.
--
--
-- PostgreSQL database dump
--
--
-- Name: countries; Type: DATABASE; Schema: -; Owner: postgres
--



--
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

--
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

--
-- Name: countries; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.countries (
    code text NOT NULL,
    name text NOT NULL,
    continent text,
    description text
);


ALTER TABLE public.countries OWNER TO postgres;

--
-- Name: data; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.data (
    d_id bigint NOT NULL,
    name text NOT NULL,
    description text,
    unit text NOT NULL,
    country text
);


ALTER TABLE public.data OWNER TO postgres;

--
-- Name: data_d_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.data_d_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.data_d_id_seq OWNER TO postgres;

--
-- Name: data_d_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.data_d_id_seq OWNED BY public.data.d_id;


--
-- Name: datapoints; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.datapoints (
    dp_id bigint NOT NULL,
    date date NOT NULL,
    value double precision NOT NULL,
    dp_id1 bigint
);


ALTER TABLE public.datapoints OWNER TO postgres;

--
-- Name: datapoints_dp_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.datapoints_dp_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.datapoints_dp_id_seq OWNER TO postgres;

--
-- Name: datapoints_dp_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.datapoints_dp_id_seq OWNED BY public.datapoints.dp_id;


--
-- Name: data d_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.data ALTER COLUMN d_id SET DEFAULT nextval('public.data_d_id_seq'::regclass);


--
-- Name: datapoints dp_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.datapoints ALTER COLUMN dp_id SET DEFAULT nextval('public.datapoints_dp_id_seq'::regclass);

--
-- Name: data_d_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.data_d_id_seq', 1, true);


--
-- Name: datapoints_dp_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.datapoints_dp_id_seq', 1, true);


--
-- Name: countries pk_countries; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.countries
    ADD CONSTRAINT pk_countries PRIMARY KEY (code);


--
-- Name: data pk_data; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.data
    ADD CONSTRAINT pk_data PRIMARY KEY (d_id);


--
-- Name: datapoints pk_datapoints; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.datapoints
    ADD CONSTRAINT pk_datapoints PRIMARY KEY (dp_id);



--
-- Name: data fk_data_countries_country; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.data
    ADD CONSTRAINT fk_data_countries_country FOREIGN KEY (country) REFERENCES public.countries(code);


--
-- Name: datapoints fk_datapoints_data_dp_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.datapoints
    ADD CONSTRAINT fk_datapoints_data_dp_id FOREIGN KEY (dp_id1) REFERENCES public.data(d_id);


--
-- PostgreSQL database dump complete
--

