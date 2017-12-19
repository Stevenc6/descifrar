Imports MySql.Data.MySqlClient

Public Class claseConexion
    'VERSION 1.3.8.072011
    Private conexion As MySqlConnection
    Private sConexion As String 'el string de conexion que se esta utilizando
    Private query As MySqlCommand
    Private daDatos As MySqlDataAdapter

    Private transaccion As MySqlTransaction
    'Esto es para saber en que estado esta la transaccion
    Private EstadoTransaccion As Integer
    Private Enum Estado As Integer
        Iniciado = 1
        Finalizado = 0
        ConError = 2
    End Enum

    'variable utilizada para cuando insertamos datos ...
    Dim _UltimoIdInsertado As Long
    Public ReadOnly Property UltimoIdInsertado() As Long
        Get
            Return _UltimoIdInsertado
        End Get
    End Property

    Public Sub New()
        conexion = New MySqlConnection
        InstanciarQuery()
    End Sub


#Region "--- Propiedades de la conexion ..."
    Private _sServer As String = String.Empty
    Private _sInitialCatalog As String = String.Empty
    Private _sUserId As String = String.Empty
    Private _sPassword As String = String.Empty
    Private _iPuerto As Integer = 3306
    Private _bPooling As Boolean = False
    Private _bAllowZeroDateTime As Boolean = False

    ''' <summary>
    ''' Aqui va la ip del server al cual se va a conectar...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Server() As String
        Get
            Return _sServer
        End Get
        Set(ByVal value As String)
            _sServer = value
        End Set
    End Property

    ''' <summary>
    ''' A que base se va a conectar ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property InitialCatalog() As String
        Get
            Return _sInitialCatalog
        End Get
        Set(ByVal value As String)
            _sInitialCatalog = value
        End Set
    End Property

    ''' <summary>
    ''' Usuario que se utiliza para la conexion ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property UserId() As String
        Get
            Return _sUserId
        End Get
        Set(ByVal value As String)
            _sUserId = value
        End Set
    End Property

    ''' <summary>
    ''' Clave a utilizarse en la conexion ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Password() As String
        Get
            Return _sPassword
        End Get
        Set(ByVal value As String)
            _sPassword = value
        End Set
    End Property

    ''' <summary>
    ''' Numero de puerto que va a utilizar la conexion ...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Puerto() As Integer
        Get
            Return _iPuerto
        End Get
        Set(ByVal value As Integer)
            _iPuerto = value
        End Set
    End Property

    ''' <summary>
    ''' Configuracion de la conexion si va a utilizar pooling
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property pooling() As Boolean
        Get
            Return _bPooling
        End Get
        Set(ByVal value As Boolean)
            _bPooling = value
        End Set
    End Property

    ''' <summary>
    ''' Configuracion de la conexion si va a aceptar fechas tipo string...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property AllowZeroDateTime() As Boolean
        Get
            Return _bAllowZeroDateTime
        End Get
        Set(ByVal value As Boolean)
            _bAllowZeroDateTime = value
        End Set
    End Property
#End Region

#Region "--- Funciones utilizados para inicializar la conexion hacia la base de datos ..."
    ''' <summary>
    ''' String con la direccion de mysql a utilizar en la conexion...
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property StringDeConexion() As String
        Get
            Return sConexion
        End Get
        Set(ByVal value As String)
            sConexion = value
            conexion.ConnectionString = sConexion
        End Set
    End Property

    ''' <summary>
    ''' Abrir la conexion con la base de datos...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub AbrirConexion()
        If conexion.State = ConnectionState.Closed Then
            conexion.Open()
        End If
    End Sub

    ''' <summary>
    ''' Cerrar la conexion con la base de datos...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub CerrarConexion()
        conexion.Close()
    End Sub

    ''' <summary>
    ''' Se obtiene la variable de conexion que se esta utilizando...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerConexion() As MySqlConnection
        Return conexion
    End Function

    ''' <summary>
    ''' Se obtiene que string de conexion se esta utilizando...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ObtenerStringDeConexion() As String
        Return sConexion
    End Function

    ''' <summary>
    ''' Por si se quiere instanciar nuevamente el command...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub InstanciarQuery()
        query = New MySqlCommand
        query.Connection = conexion
        daDatos = New MySqlDataAdapter
    End Sub

    ''' <summary>
    ''' Con las propiedades de la conexion ingresados de antemano se genera el estring de conexion...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GenerarConexion()
        StringDeConexion = "server = " & _sServer & ";" & _
        " initial catalog = " & _sInitialCatalog & ";" & _
        " user id = " & _sUserId & ";" & _
        " password = " & _sPassword & ";" & _
        " port = " & _iPuerto & ";" & _
        " pooling = " & _bPooling & ";" & _
        " allow zero datetime = " & _bAllowZeroDateTime & ";"
    End Sub

    ''' <summary>
    ''' Se busca un xml con la informacion de la conexion en la ubicacion del exe...
    ''' </summary>
    ''' <param name="nombrexml"></param>
    ''' <remarks></remarks>
    Public Sub BuscarConexionEnXml(ByVal nombrexml As String)

        Try
            Dim dsconexion As New DataSet

            dsconexion.ReadXml(nombrexml)

            'AHORA asignamos a las propiedades de la conexion la info...
            AsignarDatosDConexion(dsconexion)

            GenerarConexion()

        Catch ex As Exception

            Throw New InvalidOperationException("Error en el archivo de conexion " & nombrexml & " : " & ex.Message)

            Throw ex

        End Try

    End Sub

    ''' <summary>
    ''' Se busca un xml con la informacion de la conexion en la ubicacion especificada...
    ''' </summary>
    ''' <param name="nombrexml"></param>
    ''' <param name="ubicacionxml"></param>
    ''' <remarks></remarks>
    Public Sub BuscarConexionEnXml(ByVal nombrexml As String, ByVal ubicacionxml As String)
        Try
            Dim dsconexion As New DataSet

            dsconexion.ReadXml(ubicacionxml & "\" & nombrexml)

            'AHORA asignamos a las propiedades de la conexion la info...
            AsignarDatosDConexion(dsconexion)

            GenerarConexion()

        Catch ex As Exception

            Throw New InvalidOperationException("Error en el archivo de conexion " & nombrexml & " : " & ex.Message)

            Throw ex

        End Try


    End Sub

    Private Sub AsignarDatosDConexion(ByVal dsconexion As DataSet)
        _sServer = dsconexion.Tables(0).Rows(0)(0)
        _sInitialCatalog = dsconexion.Tables(0).Rows(0)(1)
        _sUserId = dsconexion.Tables(0).Rows(0)(2)
        _sPassword = descubrirpassword(dsconexion.Tables(0).Rows(0)(3))
        If dsconexion.Tables(0).Columns.Count >= 5 Then
            'no todos los archivos de conexion tienen las 5 columnas ...
            _iPuerto = dsconexion.Tables(0).Rows(0)(4)
        End If
    End Sub
    Private Function descubrirpassword(ByVal spentrada As String) As String

        Dim caracteres As String
        Dim codigo As Integer
        Dim stemp As String
        Dim cadenadesencriptada As String = ""
        Dim j As Integer

        For j = 0 To (spentrada.Length - 1) Step 3

            caracteres = spentrada.Chars(j + 0) & spentrada.Chars(j + 1) & spentrada.Chars(j + 2)

            codigo = (caracteres)

            stemp = Chr(codigo)

            cadenadesencriptada = cadenadesencriptada & stemp

            If j + 3 > spentrada.Length - 1 Then

                j = spentrada.Length

            End If
        Next

        Return cadenadesencriptada

    End Function
#End Region

#Region "--- Funciones utilizadas cuando hay una transaccion ..."

    ''' <summary>
    ''' se inicia con la transaccion en los querys de Mysql...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub IniciarTransaccion()
        Try
            AbrirConexion()
            transaccion = conexion.BeginTransaction
            'le asignamos al comand la transaccion ...
            query.Transaction = transaccion
            'indicamos que hemos iniciado la transaccion...
            EstadoTransaccion = Estado.Iniciado
        Catch ex As Exception
            Throw New InvalidOperationException(" Iniciando transaccion: " & ex.Message)
            Throw ex
        End Try
    End Sub

    ''' <summary>
    ''' Se finaliza la transaccion, si no existe error ejecuta Commit, de lo contrario Rollback...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub FinalizarTransaccion()
        Try
            If EstadoTransaccion = Estado.ConError Then
                transaccion.Rollback()
            Else
                'aquiere decir que no hay error...
                If EstadoTransaccion = Estado.Iniciado Then
                    transaccion.Commit()
                End If
            End If
        Catch ex As Exception
            'AQUI PUEDE DAR UN ERROR, si es que no hay conexion, pero de ser asi mysql hace rollback...
        Finally
            'indicamos que finalizo la transaccion...
            EstadoTransaccion = Estado.Finalizado
            'indicamos que el query ya no tenga ninguna transaccion 
            query.Transaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Cambia el estado de la transaccion, para realizar el rollback al Finalizar la Transaccion...
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ForzarErrorTransaccion()
        EstadoTransaccion = Estado.ConError
    End Sub

#End Region

#Region "--- Funciones utilizados para ejecutar querys y traer informacion ..."

    Private Sub LimpiarQuery_CommandText()
        query.CommandText = String.Empty
    End Sub

    ''' <summary>
    ''' Ejecuta el query deseado, y nos devuelve el numero de lineas afectadas ...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarNonQuery(ByVal _squery As String) As Integer
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery

            'ahora ejecutamos el query...
            EjecutarNonQuery = query.ExecuteNonQuery

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' Devuelve un dato ...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EjecutarEscalar(ByVal _squery As String) As Object
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            'traemos el dato solicitado...
            query.CommandText = _squery
            EjecutarEscalar = query.ExecuteScalar

        Catch ex As Exception

            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' Esta funcion nos sirve para cuando insertamos un datos, y asi saber el id nuevo, 
    ''' Para eso se utilizo la variabla UltimoIdInsertado. SIEMPRE Y CUANDO EL ID SEA NUMERICO ...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertarDato(ByVal _squery As String) As Integer
        Dim tmpEstadoConexion As Integer = 0
        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            'limpiamos nuestro ultimo id insertado...
            _UltimoIdInsertado = 0

            query.CommandText = _squery
            'ahora ejecutamos el query...
            InsertarDato = query.ExecuteNonQuery

            'aqui es donde sabemos que id utilizo el query al final...
            _UltimoIdInsertado = query.LastInsertedId

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function


    ''' <summary>
    ''' LLENA UN DATASET POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO
    ''' SI LA CONEXION NO ESTA ABIERTA ESTA FUNCION LA ABRE EJECUTA EL 
    ''' LLENADO DEL DATASET Y LA CIERRA
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataSet(ByVal _squery As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(datasettemporal)

            Return datasettemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try
    End Function

    ''' <summary>
    ''' LLENA UN DATASET POR MEDIO DE UN DATA ADAPTER USANDO EL 
    ''' QUERY ENVIADO COMO PARAMETRO, SE PUEDE ESPECIFICAR EL NOMBRE QUE VA A TENER 
    ''' LA TABLA EN EL DATASET
    ''' SI LA CONEXION NO ESTA ABIERTA ESTA FUNCION LA ABRE EJECUTA EL 
    ''' LLENADO DEL DATASET Y LA CIERRA
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <param name="nombretabla"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function llenarDataSet(ByVal _squery As String, ByVal nombretabla As String) As DataSet

        Dim tmpEstadoConexion As Integer = 0
        Dim datasettemporal As New DataSet

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(datasettemporal, nombretabla)

            Return datasettemporal

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try

    End Function

    ''' <summary>
    ''' Este no retorna dataset, sino que agrega la info. al dataset enviado por referencia...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <param name="nombretabla"></param>
    ''' <param name="DsDatos"></param>
    ''' <remarks></remarks>
    Public Sub llenarDataSet(ByVal _squery As String, ByVal nombretabla As String, ByRef DsDatos As DataSet)

        Dim tmpEstadoConexion As Integer = 0

        Try
            'si la conexion esta cerrada entonces la abrimos...
            If conexion.State = ConnectionState.Closed Then
                conexion.Open()
                tmpEstadoConexion = 1
            End If

            query.CommandText = _squery
            daDatos.SelectCommand = query

            daDatos.Fill(DsDatos, nombretabla)

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException(ex.Message)
            Throw ex
        Finally
            'si la conexion estaba cerrada entonces la cerramos...
            If tmpEstadoConexion = 1 Then
                conexion.Close()
            End If
            LimpiarQuery_CommandText()
        End Try

    End Sub

#End Region

#Region "--- Funciones de verificacion de conexion"

    ''' <summary>
    ''' Para ver si hay ping con el servidor especificado...
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Ping() As Boolean
        Try
            'se hacen varios intentos de ping...
            For i As Integer = 0 To 4
                If My.Computer.Network.Ping(_sServer) Then
                    'si logra hacer ping al servidor entonces retorna verdadero...
                    'Return True
                    'ahora abrimos la conexion!
                    'En teoria si no tira erros al abrir la conexion entonces se puede usar la conexion...
                    Try
                        AbrirConexion()
                        Return True
                    Catch ex As Exception
                        'no vamos a mostrar error porque solo se esta intentando establecer si se puede
                        'insertar informacion...
                    Finally
                        CerrarConexion()
                    End Try
                End If
                'se espera antes de intentar otra vez...
                System.Threading.Thread.Sleep(2000)
            Next
            Return False
        Catch ex As Exception
            Throw New InvalidOperationException("Error buscando servidor. " & ex.Message)
            Throw ex
        End Try
    End Function
#End Region


#Region "--- Funciones para ejecutar el DataReader"
    Private _drLector As MySqlDataReader
    Public ReadOnly Property drLector() As MySqlDataReader
        Get
            Return _drLector
        End Get
    End Property

    ''' <summary>
    ''' Se inicia el dataReader con el query necesario...
    ''' </summary>
    ''' <param name="_squery"></param>
    ''' <remarks></remarks>
    Public Sub iniciarDataReader(ByVal _squery As String)
        Try
            query.CommandText = _squery

            'ya deberia estar abierta la conexion...
            _drLector = query.ExecuteReader

        Catch ex As Exception
            'si se esta ejecutando con transaccion entonces se indica que hubo error.
            If EstadoTransaccion = Estado.Iniciado Then
                EstadoTransaccion = Estado.ConError
            End If

            Throw New InvalidOperationException("Error inicializando el DataReader. " & ex.Message)
            Throw ex
        End Try
    End Sub

    Public Sub cerrarDataReader()
        _drLector.Close()
    End Sub

#End Region

End Class
