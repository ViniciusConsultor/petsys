Imports Schedule

Namespace Schedule

    Public MustInherit Class Schedule

        Protected agendador As ScheduleTimer
        Private _segundos As Double

        Private m_Executando As Boolean
        Public Property Executando() As Boolean
            Get
                Return m_Executando
            End Get
            Private Set(value As Boolean)
                m_Executando = value
            End Set
        End Property

        Protected MustOverride Sub Inicialize()

        Private Sub ConfigureAgendador()
            Dim tarefaDoSubServico As [Delegate] = New MetodoSimplesHandler(AddressOf ExecuteTarefa)
            Dim item As IScheduledItem = New SimpleInterval(DateTime.Now, TimeSpan.FromSeconds(_segundos))
            agendador.AddAsyncJob(item, tarefaDoSubServico)
        End Sub

        Public MustOverride ReadOnly Property Nome() As String

        Public Sub RegistreErro(erro As String)
            Logger.GetInstancia().Erro("Sub-Serviço " & Nome & " " & vbLf & erro)
        End Sub

        Private Function ExecuteEmModoSeguro(metodo As MetodoSimplesHandler) As Boolean
            Dim executado As Boolean = False

            Try
                metodo()
                executado = True
            Catch ex As Exception
                Executando = False
                RegistreErro(ex.ToString())
            End Try

            Return executado
        End Function

        Public Function Inicie(segundos As Nullable(Of Double)) As Boolean
            _segundos = 300

            If Not segundos Is Nothing Then
                _segundos = segundos.Value
            End If

            Return ExecuteEmModoSeguro(AddressOf InicieServico)
        End Function

        Public Function Termine() As Boolean
            Return ExecuteEmModoSeguro(AddressOf TermineServico)
        End Function

        Protected Delegate Sub MetodoSimplesHandler()

        Protected Sub InicieServico()
            Inicialize()
            agendador = New ScheduleTimer()
            ConfigureAgendador()

            Executando = True
            AddHandler agendador.[Error], AddressOf agendador_Error
            agendador.Start()
        End Sub

        Private Sub agendador_Error(sender As Object, Args As ExceptionEventArgs)
            Executando = False
            RegistreErro(Args.[Error].ToString())
        End Sub

        Protected Sub TermineServico()
            Executando = False
            agendador.[Stop]()
        End Sub

        Protected MustOverride Sub ExecuteTarefa()

    End Class

End Namespace