﻿Namespace Core.Negocio.Telefone

    Public Interface ITelefone

        Property ID() As Nullable(Of Long)
        Property DDD() As Short
        Property Numero() As Long
        Property Tipo() As TipoDeTelefone

    End Interface

End Namespace