#include <stdio.h>
#include <math.h>

int main()
{
    long num;
    long temp1;
    short i, factcount;
    long factors[30];
    short exps[30];
    long smars[30];
    long max;
    long divisor;
    long maxcheck;
    unsigned char found;
    long startnum;
    long sum_of_factor;
    long tempsum;

    printf("Enter the value of the number\n");
    scanf("%ld", &num);
    if (num == 1)
    {
        printf("1 1\n");
    }
    else
    {
        factcount = 0;
        for (i = 0; i < 30; i++)
        {
            factors[i] = 0;
            smars[i] = 0;
            exps[i] = 0;
        }

        /* The first step is to break num into equivalent prime factorization. For effieciency considerations
           we first remove all instances of the even prime 2.*/

        temp1 = num;
        if ((num % 2) == 0)
        {
            temp1 = num / 2;
            factors[0] = 2;
            exps[0] = 1;
            while (((temp1 % 2) == 0) && (temp1 > 1))
            {
                temp1 = temp1 / 2;
                exps[0]++;
            }
            factcount = 1;
        }

        maxcheck = (long)sqrt(num);
        divisor = 3;
        while ((temp1 > 1) && (divisor <= maxcheck))
        {
            if ((temp1 % divisor) == 0)
            {
                factors[factcount] = divisor;
                exps[factcount] = 1;
                temp1 = temp1 / divisor;
                while (((temp1 % divisor) == 0) && (temp1 > 1))
                {
                    temp1 = temp1 / divisor;
                    exps[factcount]++;
                }
                factcount++;
            }
            divisor = divisor + 2;
        }

        // If temp1>1 at this point, then num is prime
        if (temp1 > 1)
        {
            factcount = 1;
            factors[factcount] = num;
            exps[factcount] = 1;
        }

        /* The next step is to compute the value of the Smarandache function for each pair of
           entries in the arrays. */

        for (i = 0; i < factcount; i++) {
            if (exps[i] < factors[i]) {
                smars[i] = exps[i] * factors[i];
            } else {
                startnum = exps[i] / factors[i];
                if (startnum < 1) {
                    startnum = 1;
                }
                found = 0;
                while (found == 0) {
                    sum_of_factor = startnum;
                    tempsum = startnum / factors[i];
                    while (tempsum > 0) {
                        sum_of_factor = sum_of_factor + tempsum;
                        tempsum = tempsum / factors[i];
                    }

                    if (sum_of_factor >= exps[i]) {
                        found = 1;
                    } else {
                        startnum++;
                    }
                }
                smars[i] = startnum * factors[i];
            }
        }

        /* The final step is to determine the largest of the values of the Smarandache function. */
        max = 0;
        for (i = 0; i < factcount; i++)
        {
            if (smars[i] > max)
            {
                max = smars[i];
            }
        }
        printf("%ld %ld\n", num, max);
    }
    return 0;
}