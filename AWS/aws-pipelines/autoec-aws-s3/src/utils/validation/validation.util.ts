
import {cnpj as cnpjValidator} from 'cpf-cnpj-validator';
import * as yup from 'yup';
import {onlyNumbersMask} from '..';

enum Messages {
  InvalidAccount = 'Conta inválida.',
  InvalidAccountDigit = 'Dígito inválido.',
  InvalidAgency = 'Agência inválida.',
  InvalidBank = 'Banco inválido.',
  InvalidCNPJ = 'CNPJ inválido.',
  InvalidDate = 'Data inválida.',
  InvalidEmail = 'E-mail inválido',
  InvalidField = 'Campo inválido',
  InvalidPhone = 'Telefone inválido.',
  InvalidZipCode = 'CEP inválido.',
  RequiredField = 'Campo obrigatório.',
}

export const base = () => yup.object();

export const custom = () => yup;

export const name = () => yup
  .string()
  .trim()
  .required(Messages.RequiredField)
  .min(2, 'Nomes devem possuir 2 ou mais caracteres.')
  .max(256, 'Nomes devem possuir no máximo 256 caracteres.');

export const cnpj = () => yup
  .string()
  .trim()
  .required(Messages.RequiredField)
  .test({
    test: (value = '') => {
      const cleaned = onlyNumbersMask(value);
      return cnpjValidator.isValid(cleaned);
    },
    message: Messages.InvalidCNPJ,
  });

export const dateStr = () => yup
  .string()
  .trim()
  .required(Messages.RequiredField)
  .test({
    test: (value = '') => {
      let date;
      try {
        date = new Date(value);
      } catch(error) {
        return false;
      }

      return !isNaN(date.getTime());
    },
    message: Messages.InvalidDate,
  });

export const exists = () => yup
  .string()
  .trim()
  .required(Messages.RequiredField);

export const email = () => yup
  .string()
  .trim()
  .email(Messages.InvalidEmail)
  .required(Messages.RequiredField);

export const phone = () => yup
  .string()
  .trim()
  .required(Messages.RequiredField)
  .test({
    test: (value = '') => {
      const {length} = onlyNumbersMask(value);
      return (length === 10 || length === 11);
    },
    message: Messages.InvalidPhone,
  });

export const zipCode = () => yup
  .string()
  .trim()
  .required(Messages.RequiredField)
  .test({
    test: (value = '') => {
      const cleaned = onlyNumbersMask(value);
      return cleaned.length === 8;
    },
    message: Messages.InvalidZipCode,
  });

export const isTrue = () => yup
  .boolean()
  .isTrue();

export const length = ({min, max}: {
  min: number;
  max: number;
}) => yup
  .string()
  .trim()
  .min(min, Messages.InvalidField)
  .max(max, Messages.InvalidField)
  .required(Messages.RequiredField);

export const bank = ({options}: { options?: Array<string>; } = { }) => yup
  .string()
  .trim()
  .min(3, Messages.InvalidBank)
  .max(128, Messages.InvalidBank)
  .test({
    test: value => {
      return options ? options?.includes(value) : true;
    },
    message: Messages.InvalidBank,
  });

export const agency = () => yup
  .string()
  .trim()
  .min(4, Messages.InvalidAgency)
  .max(5, Messages.InvalidAgency)
  .required(Messages.RequiredField);

export const account = () => yup
  .string()
  .trim()
  .min(4, Messages.InvalidAccount)
  .max(5, Messages.InvalidAccount)
  .required(Messages.RequiredField);

export const accountDigit = () => yup
  .string()
  .trim()
  .length(1, Messages.InvalidAccountDigit)
  .required(Messages.RequiredField);
