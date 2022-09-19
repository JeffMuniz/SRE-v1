
export const toDateString = ({
  date,
  withTime = false,
  locale = 'pt-br',
}: {
  date: Date;
  withTime?: false;
  locale?: 'pt-br'
}): string => {
  let parsed;

  if(withTime) {
    parsed = date.toLocaleString(locale);
  } else {
    parsed = date.toLocaleDateString(locale);
  }

  return parsed;
};
