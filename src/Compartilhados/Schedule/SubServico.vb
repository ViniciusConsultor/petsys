Imports Schedule

Namespace Schedule

    Public MustInherit Class SubServico

        Protected agendador As ScheduleTimer

        Public Property Executando() As Boolean
            Get
                Return m_Executando
            End Get
            Private Set(value As Boolean)
                m_Executando = Value
            End Set
        End Property
        Private m_Executando As Boolean

        Protected MustOverride Sub Inicialize()

        Protected MustOverride Sub ConfigureAgendador()

        Public MustOverride ReadOnly Property Nome() As String

        Public Sub RegistreErro(erro As String)
            'Logger.GetInstancia().Erro("Sub-Serviço " & Nome & " " & erro,)
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

        Public Function Inicie() As Boolean
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