USE [Trackademi]
GO
/****** Object:  Table [dbo].[Calificaciones]    Script Date: 23/7/2025 8:11:49 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calificaciones](
	[idCalificacion] [int] IDENTITY(1,1) NOT NULL,
	[idMateria] [int] NOT NULL,
	[idEstudiante] [int] NOT NULL,
	[NotaParcial] [int] NOT NULL,
	[Observaciones] [varchar](max) NULL,
	[NotaFinal] [int] NULL,
 CONSTRAINT [PK_Calificaciones] PRIMARY KEY CLUSTERED 
(
	[idCalificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ControlAsistencia]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlAsistencia](
	[idAsistencia] [int] IDENTITY(1,1) NOT NULL,
	[idMateria] [int] NOT NULL,
	[idEstudiante] [int] NOT NULL,
	[Presente] [bit] NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_Asistencias] PRIMARY KEY CLUSTERED 
(
	[idAsistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiantes]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiantes](
	[idEstudiante] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Matricula] [varchar](15) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Telefono] [varchar](15) NULL,
	[Genero] [char](1) NOT NULL,
	[FechaNacimiento] [date] NULL,
	[FechaCreacion] [datetime] NULL,
 CONSTRAINT [PK_Estudiantes] PRIMARY KEY CLUSTERED 
(
	[idEstudiante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[idLog] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[Metodo] [varchar](20) NOT NULL,
	[URL] [varchar](500) NOT NULL,
	[Data] [varchar](max) NOT NULL,
	[Fecha] [datetime] NOT NULL,
 CONSTRAINT [PK_LogActividades] PRIMARY KEY CLUSTERED 
(
	[idLog] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materia]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[idPeriodo] [int] NOT NULL,
	[Detalle] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Materias] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MateriaInscrita]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MateriaInscrita](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idEstudiante] [int] NOT NULL,
	[idMateria] [int] NOT NULL,
 CONSTRAINT [PK_EstudianteAsignatura] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Perfiles]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfiles](
	[idPerfil] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](350) NULL,
 CONSTRAINT [PK_Perfiles] PRIMARY KEY CLUSTERED 
(
	[idPerfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Periodos]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Periodos](
	[idPeriodo] [int] IDENTITY(1,1) NOT NULL,
	[Codigo] [varchar](150) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
 CONSTRAINT [PK_Periodos] PRIMARY KEY CLUSTERED 
(
	[idPeriodo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 23/7/2025 8:11:50 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](50) NOT NULL,
	[Clave] [varchar](256) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Nombres] [varchar](100) NOT NULL,
	[Apellidos] [varchar](100) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Activo] [bit] NOT NULL,
	[idPerfil] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Calificaciones] ON 
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (1, 1, 1, 85, N'Buen rendimiento', 70)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (2, 2, 2, 80, N'Estudiante que se esforzó ', 85)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (3, 1, 2, 69, N'', 80)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (4, 4, 1, 60, N'', 70)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (5, 4, 2, 50, N'', 80)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (6, 4, 3, 100, N'', 100)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (7, 4, 4, 95, N'', 96)
GO
INSERT [dbo].[Calificaciones] ([idCalificacion], [idMateria], [idEstudiante], [NotaParcial], [Observaciones], [NotaFinal]) VALUES (8, 4, 5, 80, N'', 97)
GO
SET IDENTITY_INSERT [dbo].[Calificaciones] OFF
GO
SET IDENTITY_INSERT [dbo].[ControlAsistencia] ON 
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (1, 4, 0, 0, CAST(N'2025-07-04T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (2, 4, 0, 0, CAST(N'2025-07-04T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (3, 4, 0, 0, CAST(N'2025-07-04T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (4, 4, 0, 0, CAST(N'2025-07-04T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (5, 4, 0, 0, CAST(N'2025-07-04T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (6, 4, 1, 1, CAST(N'2025-07-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (7, 4, 2, 1, CAST(N'2025-07-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (8, 4, 3, 0, CAST(N'2025-07-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (9, 4, 4, 1, CAST(N'2025-07-23T00:00:00.000' AS DateTime))
GO
INSERT [dbo].[ControlAsistencia] ([idAsistencia], [idMateria], [idEstudiante], [Presente], [Fecha]) VALUES (10, 4, 5, 0, CAST(N'2025-07-23T00:00:00.000' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[ControlAsistencia] OFF
GO
SET IDENTITY_INSERT [dbo].[Estudiantes] ON 
GO
INSERT [dbo].[Estudiantes] ([idEstudiante], [idUsuario], [Nombres], [Apellidos], [Matricula], [Email], [Telefono], [Genero], [FechaNacimiento], [FechaCreacion]) VALUES (1, 0, N'juan', N'Vázquez', N'2013', N'admin@edugestion360.com', N'8295912481', N'M', CAST(N'2025-07-02' AS Date), CAST(N'2025-07-21T10:51:11.307' AS DateTime))
GO
INSERT [dbo].[Estudiantes] ([idEstudiante], [idUsuario], [Nombres], [Apellidos], [Matricula], [Email], [Telefono], [Genero], [FechaNacimiento], [FechaCreacion]) VALUES (2, 0, N'juan', N'quezada', N'2013-12', N'admin@edugestion360.com', N'8295912481', N'M', CAST(N'2010-01-30' AS Date), CAST(N'2025-07-22T00:06:27.770' AS DateTime))
GO
INSERT [dbo].[Estudiantes] ([idEstudiante], [idUsuario], [Nombres], [Apellidos], [Matricula], [Email], [Telefono], [Genero], [FechaNacimiento], [FechaCreacion]) VALUES (3, 0, N'Mayelin', N'Muñoz', N'2013-17', N'admin@edugestion360.com', N'8295912481', N'F', CAST(N'2025-07-31' AS Date), CAST(N'2025-07-23T01:56:22.427' AS DateTime))
GO
INSERT [dbo].[Estudiantes] ([idEstudiante], [idUsuario], [Nombres], [Apellidos], [Matricula], [Email], [Telefono], [Genero], [FechaNacimiento], [FechaCreacion]) VALUES (4, 0, N'Carlos', N'Lora', N'2013-18', N'admin@edugestion360.com', N'8295912481', N'M', CAST(N'2025-07-29' AS Date), CAST(N'2025-07-23T01:56:55.710' AS DateTime))
GO
INSERT [dbo].[Estudiantes] ([idEstudiante], [idUsuario], [Nombres], [Apellidos], [Matricula], [Email], [Telefono], [Genero], [FechaNacimiento], [FechaCreacion]) VALUES (5, 0, N'Eurfridia', N'Algarrobo', N'2013-20', N'admin@edugestion360.com', N'8295912481', N'F', CAST(N'2025-07-31' AS Date), CAST(N'2025-07-23T01:57:31.607' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Estudiantes] OFF
GO
SET IDENTITY_INSERT [dbo].[Materia] ON 
GO
INSERT [dbo].[Materia] ([id], [Nombre], [idPeriodo], [Detalle]) VALUES (1, N'Matematicas', 3, N'Calculo Basico')
GO
INSERT [dbo].[Materia] ([id], [Nombre], [idPeriodo], [Detalle]) VALUES (2, N'Sociales', 3, N'Materia de Historia Dominicana')
GO
INSERT [dbo].[Materia] ([id], [Nombre], [idPeriodo], [Detalle]) VALUES (3, N'Naturales', 3, N'Ciencias de la Naturaleza')
GO
INSERT [dbo].[Materia] ([id], [Nombre], [idPeriodo], [Detalle]) VALUES (4, N'Literatura', 1, N'Redacción y Lectura')
GO
SET IDENTITY_INSERT [dbo].[Materia] OFF
GO
SET IDENTITY_INSERT [dbo].[MateriaInscrita] ON 
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (3, 1, 1)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (4, 2, 1)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (5, 2, 2)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (6, 1, 3)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (7, 2, 3)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (8, 1, 4)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (9, 2, 4)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (10, 3, 4)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (11, 4, 4)
GO
INSERT [dbo].[MateriaInscrita] ([id], [idEstudiante], [idMateria]) VALUES (12, 5, 4)
GO
SET IDENTITY_INSERT [dbo].[MateriaInscrita] OFF
GO
SET IDENTITY_INSERT [dbo].[Perfiles] ON 
GO
INSERT [dbo].[Perfiles] ([idPerfil], [Nombre], [Descripcion]) VALUES (1, N'Adminstrador', N'Usuario con todos los permisos')
GO
INSERT [dbo].[Perfiles] ([idPerfil], [Nombre], [Descripcion]) VALUES (2, N'Profesor', N'Usuario que evalua estudiantes y se encarga de llevar un conteo de sus asistencias')
GO
INSERT [dbo].[Perfiles] ([idPerfil], [Nombre], [Descripcion]) VALUES (3, N'Moderador', N'Modera los permisos')
GO
SET IDENTITY_INSERT [dbo].[Perfiles] OFF
GO
SET IDENTITY_INSERT [dbo].[Periodos] ON 
GO
INSERT [dbo].[Periodos] ([idPeriodo], [Codigo], [idUsuario], [FechaCreacion]) VALUES (1, N'2025-A1', 1, CAST(N'2025-07-23T00:09:20.270' AS DateTime))
GO
INSERT [dbo].[Periodos] ([idPeriodo], [Codigo], [idUsuario], [FechaCreacion]) VALUES (2, N'2025-A2', 1, CAST(N'2025-07-23T00:30:03.857' AS DateTime))
GO
INSERT [dbo].[Periodos] ([idPeriodo], [Codigo], [idUsuario], [FechaCreacion]) VALUES (3, N'2025-A3', 1, CAST(N'2025-07-23T00:30:11.340' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Periodos] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 
GO
INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [Nombres], [Apellidos], [FechaCreacion], [Activo], [idPerfil]) VALUES (1, N'joq', N'1234', N'jose@gmail.com', N'jose', N'Quezada Matos', CAST(N'2025-07-18T13:06:34.603' AS DateTime), 1, 1)
GO
INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [Nombres], [Apellidos], [FechaCreacion], [Activo], [idPerfil]) VALUES (3, N'mayi', N'1234567', N'mayelin@gmail.com', N'Mayelin', N'Muñoz', CAST(N'2025-07-19T09:04:38.870' AS DateTime), 1, 1)
GO
INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [Nombres], [Apellidos], [FechaCreacion], [Activo], [idPerfil]) VALUES (4, N'uvazquez', N'ok4567', N'vazquez@gmail', N'juan', N'Vázquez', CAST(N'2025-07-19T09:05:59.060' AS DateTime), 0, 2)
GO
INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [Nombres], [Apellidos], [FechaCreacion], [Activo], [idPerfil]) VALUES (5, N'ok', N'12343423423', N'zabala@gmail.com', N'Ramon', N'zabala', CAST(N'2025-07-20T23:07:32.487' AS DateTime), 1, 2)
GO
INSERT [dbo].[Usuarios] ([idUsuario], [Usuario], [Clave], [Correo], [Nombres], [Apellidos], [FechaCreacion], [Activo], [idPerfil]) VALUES (6, N'Miranda2', N'098765jhhg', N'Miranda@gmail', N'Franciel', N'Miranda', CAST(N'2025-07-20T23:14:12.130' AS DateTime), 0, 1)
GO
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[ControlAsistencia] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Estudiantes] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Log] ADD  DEFAULT (getdate()) FOR [Fecha]
GO
ALTER TABLE [dbo].[Periodos] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [Activo]
GO
