
import StringMask from 'string-mask';

const cellphoneStringMask = new StringMask('00 00000-0000');
const cnpjStringMask = new StringMask('00.000.000/0000-00');
const cpfStringMask = new StringMask('000.000.000-00');
const phoneStringMask = new StringMask('00 0000-0000');
const zipCodeStringMask = new StringMask('00000-000');
const cpfHiddenDigitsMask = new StringMask('000.0**.***-**');

export const onlyNumbers = (value: string): string => {
  return value.replace(/\D/g, '');
};

export const cnpj = (value: string): string => {
  if(isNaN(parseInt(value[value.length - 1]))) {
    return value.substring(0, value.length - 1);
  }

  const cleaned = onlyNumbers(value);
  return cnpjStringMask.apply(cleaned);
};

export const cpf = (value: string): string => {
  if(isNaN(parseInt(value[value.length - 1]))) {
    return value.substring(0, value.length - 1);
  }
  const cleaned = onlyNumbers(value);
  return cpfStringMask.apply(cleaned);
};

export const cpfHiddenDigitsStringMask = (value: string): string => {
  if(isNaN(parseInt(value[value.length - 1]))) {
    return value.substring(0, value.length - 1);
  }
  const cleaned = onlyNumbers(value);
  return cpfHiddenDigitsMask.apply(cleaned);
};

export const phone = (value: string) => {
  if(isNaN(parseInt(value[value.length - 1]))) {
    return value.substring(0, value.length - 1);
  }
  const cleaned = onlyNumbers(value);
  if(cleaned.length > 10) {
    return cellphoneStringMask.apply(cleaned);
  }
  return phoneStringMask.apply(cleaned);
};

export const zipCode = (value: string) => {
  if(isNaN(parseInt(value[value.length - 1]))) {
    return value.substring(0, value.length - 1);
  }
  const cleaned = onlyNumbers(value);
  return zipCodeStringMask.apply(cleaned);
};

export const middleware = ({
  event,
  mask,
  handleChange,
}: MaskingUtil.MiddlewareParams) => {
  const target = event.target as HTMLInputElement;
  target.value = mask(target.value);
  handleChange(event);
};
