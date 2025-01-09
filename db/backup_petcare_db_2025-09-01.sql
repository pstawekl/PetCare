PGDMP  6            	         }            petcare    16.2    17.0 <    5           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            6           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            7           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            8           1262    17013    petcare    DATABASE     z   CREATE DATABASE petcare WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Polish_Poland.1250';
    DROP DATABASE petcare;
                     mytravel_test    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                     pg_database_owner    false            9           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                        pg_database_owner    false    4            �            1259    17073    Pets    TABLE     �   CREATE TABLE public."Pets" (
    "Name" text NOT NULL,
    "Type" text NOT NULL,
    "BirthDate" timestamp with time zone NOT NULL,
    "Owner" text NOT NULL,
    "Id" integer NOT NULL
);
    DROP TABLE public."Pets";
       public         heap r       mytravel_test    false    4            �            1259    17104    Pets_Id_seq    SEQUENCE     �   ALTER TABLE public."Pets" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Pets_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public               mytravel_test    false    224    4            �            1259    17081 	   Reminders    TABLE     �   CREATE TABLE public."Reminders" (
    "Id" integer NOT NULL,
    "PetId" integer NOT NULL,
    "Message" text NOT NULL,
    "ReminderDate" timestamp with time zone NOT NULL,
    "IsSent" boolean NOT NULL
);
    DROP TABLE public."Reminders";
       public         heap r       mytravel_test    false    4            �            1259    17080    Reminders_Id_seq    SEQUENCE     �   ALTER TABLE public."Reminders" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Reminders_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public               mytravel_test    false    226    4            �            1259    17089    Users    TABLE     �   CREATE TABLE public."Users" (
    "Id" integer NOT NULL,
    "Username" text NOT NULL,
    "Email" text NOT NULL,
    "PasswordHash" text NOT NULL
);
    DROP TABLE public."Users";
       public         heap r       mytravel_test    false    4            �            1259    17088    Users_Id_seq    SEQUENCE     �   ALTER TABLE public."Users" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Users_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public               mytravel_test    false    4    228            �            1259    17097    Visits    TABLE     �   CREATE TABLE public."Visits" (
    "Id" integer NOT NULL,
    "PetId" integer NOT NULL,
    "VisitDate" timestamp with time zone NOT NULL,
    "Description" text NOT NULL,
    "IsCompleted" boolean NOT NULL
);
    DROP TABLE public."Visits";
       public         heap r       mytravel_test    false    4            �            1259    17096    Visits_Id_seq    SEQUENCE     �   ALTER TABLE public."Visits" ALTER COLUMN "Id" ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Visits_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public               mytravel_test    false    230    4            �            1259    17067    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap r       mytravel_test    false    4            �            1259    17015    pets    TABLE     �   CREATE TABLE public.pets (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    type character varying(100),
    birthdate date,
    owner character varying(255)
);
    DROP TABLE public.pets;
       public         heap r       mytravel_test    false    4            �            1259    17014    pets_id_seq    SEQUENCE     �   CREATE SEQUENCE public.pets_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 "   DROP SEQUENCE public.pets_id_seq;
       public               mytravel_test    false    216    4            :           0    0    pets_id_seq    SEQUENCE OWNED BY     ;   ALTER SEQUENCE public.pets_id_seq OWNED BY public.pets.id;
          public               mytravel_test    false    215            �            1259    17039 	   reminders    TABLE     �   CREATE TABLE public.reminders (
    id integer NOT NULL,
    petid integer NOT NULL,
    message text NOT NULL,
    reminderdate timestamp without time zone NOT NULL,
    issent boolean DEFAULT false
);
    DROP TABLE public.reminders;
       public         heap r       mytravel_test    false    4            �            1259    17038    reminders_id_seq    SEQUENCE     �   CREATE SEQUENCE public.reminders_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.reminders_id_seq;
       public               mytravel_test    false    4    220            ;           0    0    reminders_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.reminders_id_seq OWNED BY public.reminders.id;
          public               mytravel_test    false    219            �            1259    17054    users    TABLE     �   CREATE TABLE public.users (
    id integer NOT NULL,
    username character varying(50) NOT NULL,
    email character varying(255) NOT NULL,
    passwordhash character varying(255) NOT NULL
);
    DROP TABLE public.users;
       public         heap r       mytravel_test    false    4            �            1259    17053    users_id_seq    SEQUENCE     �   CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 #   DROP SEQUENCE public.users_id_seq;
       public               mytravel_test    false    222    4            <           0    0    users_id_seq    SEQUENCE OWNED BY     =   ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;
          public               mytravel_test    false    221            �            1259    17024    visits    TABLE     �   CREATE TABLE public.visits (
    id integer NOT NULL,
    petid integer NOT NULL,
    visitdate timestamp without time zone NOT NULL,
    description text,
    iscompleted boolean DEFAULT false
);
    DROP TABLE public.visits;
       public         heap r       mytravel_test    false    4            �            1259    17023    visits_id_seq    SEQUENCE     �   CREATE SEQUENCE public.visits_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 $   DROP SEQUENCE public.visits_id_seq;
       public               mytravel_test    false    4    218            =           0    0    visits_id_seq    SEQUENCE OWNED BY     ?   ALTER SEQUENCE public.visits_id_seq OWNED BY public.visits.id;
          public               mytravel_test    false    217            w           2604    17018    pets id    DEFAULT     b   ALTER TABLE ONLY public.pets ALTER COLUMN id SET DEFAULT nextval('public.pets_id_seq'::regclass);
 6   ALTER TABLE public.pets ALTER COLUMN id DROP DEFAULT;
       public               mytravel_test    false    216    215    216            z           2604    17042    reminders id    DEFAULT     l   ALTER TABLE ONLY public.reminders ALTER COLUMN id SET DEFAULT nextval('public.reminders_id_seq'::regclass);
 ;   ALTER TABLE public.reminders ALTER COLUMN id DROP DEFAULT;
       public               mytravel_test    false    219    220    220            |           2604    17057    users id    DEFAULT     d   ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);
 7   ALTER TABLE public.users ALTER COLUMN id DROP DEFAULT;
       public               mytravel_test    false    221    222    222            x           2604    17027 	   visits id    DEFAULT     f   ALTER TABLE ONLY public.visits ALTER COLUMN id SET DEFAULT nextval('public.visits_id_seq'::regclass);
 8   ALTER TABLE public.visits ALTER COLUMN id DROP DEFAULT;
       public               mytravel_test    false    217    218    218            +          0    17073    Pets 
   TABLE DATA           L   COPY public."Pets" ("Name", "Type", "BirthDate", "Owner", "Id") FROM stdin;
    public               mytravel_test    false    224   YC       -          0    17081 	   Reminders 
   TABLE DATA           Y   COPY public."Reminders" ("Id", "PetId", "Message", "ReminderDate", "IsSent") FROM stdin;
    public               mytravel_test    false    226   �C       /          0    17089    Users 
   TABLE DATA           L   COPY public."Users" ("Id", "Username", "Email", "PasswordHash") FROM stdin;
    public               mytravel_test    false    228   UD       1          0    17097    Visits 
   TABLE DATA           \   COPY public."Visits" ("Id", "PetId", "VisitDate", "Description", "IsCompleted") FROM stdin;
    public               mytravel_test    false    230   �D       *          0    17067    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public               mytravel_test    false    223   BE       #          0    17015    pets 
   TABLE DATA           @   COPY public.pets (id, name, type, birthdate, owner) FROM stdin;
    public               mytravel_test    false    216   �E       '          0    17039 	   reminders 
   TABLE DATA           M   COPY public.reminders (id, petid, message, reminderdate, issent) FROM stdin;
    public               mytravel_test    false    220   �E       )          0    17054    users 
   TABLE DATA           B   COPY public.users (id, username, email, passwordhash) FROM stdin;
    public               mytravel_test    false    222   �E       %          0    17024    visits 
   TABLE DATA           P   COPY public.visits (id, petid, visitdate, description, iscompleted) FROM stdin;
    public               mytravel_test    false    218   F       >           0    0    Pets_Id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public."Pets_Id_seq"', 3, true);
          public               mytravel_test    false    231            ?           0    0    Reminders_Id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."Reminders_Id_seq"', 3, true);
          public               mytravel_test    false    225            @           0    0    Users_Id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public."Users_Id_seq"', 1, true);
          public               mytravel_test    false    227            A           0    0    Visits_Id_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public."Visits_Id_seq"', 3, true);
          public               mytravel_test    false    229            B           0    0    pets_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.pets_id_seq', 1, false);
          public               mytravel_test    false    215            C           0    0    reminders_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.reminders_id_seq', 1, false);
          public               mytravel_test    false    219            D           0    0    users_id_seq    SEQUENCE SET     ;   SELECT pg_catalog.setval('public.users_id_seq', 1, false);
          public               mytravel_test    false    221            E           0    0    visits_id_seq    SEQUENCE SET     <   SELECT pg_catalog.setval('public.visits_id_seq', 1, false);
          public               mytravel_test    false    217            �           2606    17087    Reminders PK_Reminders 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Reminders"
    ADD CONSTRAINT "PK_Reminders" PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public."Reminders" DROP CONSTRAINT "PK_Reminders";
       public                 mytravel_test    false    226            �           2606    17095    Users PK_Users 
   CONSTRAINT     R   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "PK_Users";
       public                 mytravel_test    false    228            �           2606    17103    Visits PK_Visits 
   CONSTRAINT     T   ALTER TABLE ONLY public."Visits"
    ADD CONSTRAINT "PK_Visits" PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public."Visits" DROP CONSTRAINT "PK_Visits";
       public                 mytravel_test    false    230            �           2606    17071 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public                 mytravel_test    false    223            ~           2606    17022    pets pets_pkey 
   CONSTRAINT     L   ALTER TABLE ONLY public.pets
    ADD CONSTRAINT pets_pkey PRIMARY KEY (id);
 8   ALTER TABLE ONLY public.pets DROP CONSTRAINT pets_pkey;
       public                 mytravel_test    false    216            �           2606    17047    reminders reminders_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.reminders
    ADD CONSTRAINT reminders_pkey PRIMARY KEY (id);
 B   ALTER TABLE ONLY public.reminders DROP CONSTRAINT reminders_pkey;
       public                 mytravel_test    false    220            �           2606    17065    users users_email_key 
   CONSTRAINT     Q   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_email_key UNIQUE (email);
 ?   ALTER TABLE ONLY public.users DROP CONSTRAINT users_email_key;
       public                 mytravel_test    false    222            �           2606    17061    users users_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public                 mytravel_test    false    222            �           2606    17063    users users_username_key 
   CONSTRAINT     W   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);
 B   ALTER TABLE ONLY public.users DROP CONSTRAINT users_username_key;
       public                 mytravel_test    false    222            �           2606    17032    visits visits_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.visits
    ADD CONSTRAINT visits_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.visits DROP CONSTRAINT visits_pkey;
       public                 mytravel_test    false    218            �           2606    17048    reminders reminders_petid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.reminders
    ADD CONSTRAINT reminders_petid_fkey FOREIGN KEY (petid) REFERENCES public.pets(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.reminders DROP CONSTRAINT reminders_petid_fkey;
       public               mytravel_test    false    4734    216    220            �           2606    17033    visits visits_petid_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.visits
    ADD CONSTRAINT visits_petid_fkey FOREIGN KEY (petid) REFERENCES public.pets(id) ON DELETE CASCADE;
 B   ALTER TABLE ONLY public.visits DROP CONSTRAINT visits_petid_fkey;
       public               mytravel_test    false    216    4734    218            +   �   x��N,(N��,�L-�4204�5��54P04�2��20�3��60��J�.MR.I,/���4�r�LO���/j12Į% ���29+Q!�2;�hS"�1W@~N"�&�6�N�6+cc=mC4��b���� Ax+      -   \   x�3�4�(��,����L"�|��̪�d �T�<�$��2/��*�����T��P��T��������X���R���3�ˈӈ�sSØ=... �>e      /   p   x�3��J,.I,/�2�K�� �L�̼�Ԣ��̲T�����N�DCC�����'�?���K��H�R#OK#���*�҂bscC<oo�T�bW?3�=... 3�#�      1   ]   x�3�4�4202�50�52U04�20�26�320�60�t�/�O��KU(�J�J-�L��LU�KT(?:;935;'(Ǚ�e�iDS�9��`J� MH>�      *   Z   x�3202504�444466����,�L�q.JM,I��3�3�2�+1212�-H���{���{���d�Tb�5131���L)�P���� v"�      #      x������ � �      '      x������ � �      )      x������ � �      %      x������ � �     