---

## `POST /api/Invite/Generate`

**Descrição:**

Gera um novo código de convite para um visitante.

**Body (JSON):**

```json
{
  "residentId": 1,
  "visitorName": "Nome do Visitante",
  "visitorPhoneNumber": 11999999999,
  "startDate": "2025-05-24T14:00:00Z",
  "daysToExpiration": 2,
  "justification": "Motivo do convite"
}

```

**Parâmetros:**

- `residentId` (int): ID do morador.
- `visitorName` (string): Nome do visitante.
- `visitorPhoneNumber` (long): Telefone do visitante.
- `startDate` (DateTime): Data/hora de início da validade do convite.
- `daysToExpiration` (int): Dias até expirar o convite.
- `justification` (string): Justificativa do convite.

**Respostas:**

- `200 OK`: Retorna o código do convite gerado (int).
- `400 Bad Request`: Retorna um objeto com a mensagem de erro.

---

## `POST /api/Invite/Validate`

**Descrição:**

Valida um código de convite.

**Body (JSON):**

```json
{
  "residentId": 1,
  "visitorId": 2,
  "code": 123456,
  "dateNow": "2025-05-24T15:00:00Z"
}
```

**Parâmetros:**

- `residentId` (int): ID do morador.
- `visitorId` (int): ID do visitante.
- `code` (int): Código do convite.
- `dateNow` (DateTime): Data/hora da validação.

**Respostas:**

- `200 OK`: "Code is valid"
- `400 Bad Request`: "Invalid or expired code"

---

## `GET /api/Invite/{residentId}/invites`

**Descrição:**

Retorna todos os convites de um morador.

**Parâmetros de rota:**

- `residentId` (int): ID do morador.

**Respostas:**

- `200 OK`: Lista de convites (`Invite[]`).
- `404 Not Found`: "No invites found for the resident"

---

## `GET /api/Invite/{residentId}/{visitorId}/{code}`

**Descrição:**

Retorna um convite específico pelo morador, visitante e código.

**Parâmetros de rota:**

- `residentId` (int): ID do morador.
- `visitorId` (int): ID do visitante.
- `code` (int): Código do convite.

**Respostas:**

- `200 OK`: Objeto do convite (`Invite`).
- `404 Not Found`: "Invite Not Found"

---
