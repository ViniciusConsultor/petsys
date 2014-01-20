Imports PetSys.Interfaces.Negocio
Imports System.Text

<Serializable()> _
Public Class Animal
    Implements IAnimal

    Private _DataDeNascimento As Nullable(Of Date)
    Private _Foto As String
    Private _Nome As String
    Private _Proprietario As IProprietarioDeAnimal
    Private _Sexo As SexoDoAnimal
    Private _Especie As Especie
    Private _Raca As String
    Private _ID As Nullable(Of Long)

    Public Property DataDeNascimento() As Date? Implements IAnimal.DataDeNascimento
        Get
            Return _DataDeNascimento
        End Get
        Set(ByVal value As Date?)
            _DataDeNascimento = value
        End Set
    End Property

    Public Property Foto() As String Implements IAnimal.Foto
        Get
            Return _Foto
        End Get
        Set(ByVal value As String)
            _Foto = value
        End Set
    End Property

    Public ReadOnly Property Idade() As String Implements IAnimal.Idade
        Get
            If Not DataDeNascimento Is Nothing Then
                Dim IdadeFormatada As New StringBuilder
                Dim Anos As Long
                Dim Meses As Long
                Dim Dias As Long

                Anos = DateDiff(DateInterval.Year, Me.DataDeNascimento.Value, Now)
                Meses = DateDiff(DateInterval.Month, Me.DataDeNascimento.Value, Now)
                Dias = DateDiff(DateInterval.Day, Me.DataDeNascimento.Value, Now)

                If Anos > 0 Then
                    If Anos = 1 Then
                        IdadeFormatada.Append(String.Concat(Anos, " ano "))
                    Else
                        IdadeFormatada.Append(String.Concat(Anos, " anos "))
                    End If

                Else
                    If Meses > 0 Then
                        If Meses = 1 Then
                            IdadeFormatada.Append(String.Concat(Meses, " mês "))
                        Else
                            IdadeFormatada.Append(String.Concat(Meses, " meses "))
                        End If

                    Else
                        If Dias > 0 Then
                            If Dias = 1 Then
                                IdadeFormatada.Append(String.Concat(Dias, " dia "))
                            Else
                                IdadeFormatada.Append(String.Concat(Dias, " dias "))
                            End If
                        End If
                    End If

                End If

                Return IdadeFormatada.ToString
            End If

            Return Nothing
        End Get
    End Property

    Public Property Nome() As String Implements IAnimal.Nome
        Get
            Return _Nome
        End Get
        Set(ByVal value As String)
            _Nome = value
        End Set
    End Property

    Public Property Proprietario() As IProprietarioDeAnimal Implements IAnimal.Proprietario
        Get
            Return _Proprietario
        End Get
        Set(ByVal value As IProprietarioDeAnimal)
            _Proprietario = value
        End Set
    End Property

    Public Property Sexo() As SexoDoAnimal Implements IAnimal.Sexo
        Get
            Return _Sexo
        End Get
        Set(ByVal value As SexoDoAnimal)
            _Sexo = value
        End Set
    End Property

    Public Property Raca() As String Implements IAnimal.Raca
        Get
            Return _Raca
        End Get
        Set(ByVal value As String)
            _Raca = value
        End Set
    End Property

    Public Property Especie() As Especie Implements IAnimal.Especie
        Get
            Return _Especie
        End Get
        Set(ByVal value As Especie)
            _Especie = value
        End Set
    End Property

    Public Property ID() As Long? Implements IAnimal.ID
        Get
            Return _ID
        End Get
        Set(ByVal value As Long?)
            _ID = value
        End Set
    End Property

End Class